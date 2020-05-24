using Pagene.Converter.FileTypes;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Converter.Tests")]
namespace Pagene.Converter
{
    /// <summary>
    /// Read and generate post and tag list from certain format and certain path.
    /// </summary>
    public class Converter
    {
        private readonly IFileSystem _fileSystem;
        private ChangeDetector _changeDetector;
        private Dictionary<string, IFileInfo> _hashFileMap;
        internal static string RealPath { get; private set; } = ".";
        public Converter(IFileSystem fileSystem, string path = ".")
        {
            _fileSystem = fileSystem;
            RealPath = string.IsNullOrEmpty(path) ? "." : path;
        }
        /// <summary>
        /// Creates instance for converting.
        /// </summary>
        public Converter(string path = ".") : this(new FileSystem(), path) { }

        /// <summary>
        /// Creates input directory if doesn't exist.
        /// </summary>
        public void Initialize()
        {
            InitDirectory($"{RealPath}/inputs/contents");
            InitDirectory($"{RealPath}/contents/files");
            InitDirectory($"{RealPath}/.hash");
            new ShortCutPal(RealPath).CreateShortcut("contents/files", "inputs/contents/files");
        }

        /// <summary>
        /// Start converting data.
        /// </summary>
        /// <remarks>See other documentation page about converting format and file path.</remarks>
        public async Task Convert()
        {
            var tagManager = new TagManager(_fileSystem);
            await ConvertPart(new MdFileType(_fileSystem, tagManager)).ConfigureAwait(false);
            if (MdFileType.Modified)
            {
                tagManager.CleanTags(_hashFileMap.Keys);
                await tagManager.Serialize().ConfigureAwait(false);
            }
        }
        private async Task ConvertPart(FileType fileType)
        {
            string dir = fileType.FilePath;
            string hashDir = $"{RealPath}/.hash/{dir}";
            var files = InitDirectory($"{RealPath}/inputs/{dir}")
                .GetFiles(fileType.Type, SearchOption.TopDirectoryOnly);
            _hashFileMap = InitDirectory(hashDir)
                .GetFiles($"{fileType.Type}.hashfile", SearchOption.TopDirectoryOnly)
                .ToDictionary(info => Path.GetFileNameWithoutExtension(info.Name), info => info);
            using var crypto = SHA1.Create();
            _changeDetector = new ChangeDetector(crypto);

            try
            {
                await Task.WhenAll(files.Select(file => ConvertFile(fileType, hashDir, crypto, file))).ConfigureAwait(false);
                _changeDetector.CleanHash(_hashFileMap.Values);
            }
            finally
            {
                crypto.Clear();
            }
        }

        private async Task ConvertFile(FileType fileType, string hashDir, HashAlgorithm crypto, IFileInfo file)
        {
            Stream fileStream = file.Open(FileMode.Open);
            Stream hashStream = null;
            byte[] hash;
            try
            {
                string fileName = file.Name;
                if (_hashFileMap.TryGetValue(file.Name, out IFileInfo hashFile))
                {
                    hashStream = hashFile.Open(FileMode.OpenOrCreate);
                    hash = await _changeDetector.DetectAsync(fileStream, hashStream).ConfigureAwait(false);
                    _hashFileMap.Remove(file.Name);
                }
                else
                {
                    hashStream = _fileSystem.File.Create($"{hashDir}/{file.Name}.hashfile");
                    hash = crypto.ComputeHash(fileStream);
                }
                if (hash != null)
                {
                    await fileType.SaveAsync(file, fileStream).ConfigureAwait(false);
                    await _changeDetector.WriteHash(hash, hashStream).ConfigureAwait(false);
                }
            }
            finally
            {
                fileStream.Close();
                hashStream?.Close();
            }
        }

        private IDirectoryInfo InitDirectory(string path)
        {
            var info = _fileSystem.DirectoryInfo.FromDirectoryName(path);
            if (!info.Exists)
            {
                return _fileSystem.Directory.CreateDirectory(path);
            }
            else
            {
                return info;
            }
        }
    }
}
