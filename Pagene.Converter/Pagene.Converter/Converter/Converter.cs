using Pagene.Converter.FileTypes;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Pagene.BlogSettings;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Converter.Tests")]
namespace Pagene.Converter
{
    /// <summary>
    /// Read and generate post and tag list from certain format and certain path.
    /// </summary>
    public partial class Converter
    {
        private readonly IFileSystem _fileSystem;
        private ChangeDetector _changeDetector;
        private Dictionary<string, IFileInfo> _hashFileMap;
        internal Converter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        /// <summary>
        /// Creates instance for converting.
        /// </summary>
        public Converter() : this(new FileSystem())
        {
            AppConfigLoader.LoadConfig();
        }

        /// <summary>
        /// Start converting data.
        /// </summary>
        /// <remarks>See other documentation page about converting format and file path.</remarks>
        public async Task BuildAsync()
        {
            var tagManager = new TagManager(_fileSystem);
            var mdFileType = new PostFileType(_fileSystem, tagManager);
            await BuildPartAsync(mdFileType).ConfigureAwait(false);
        }
        private async Task BuildPartAsync(FileType fileType)
        {
            string hashDir = Path.Combine(AppPathInfo.BlogHashPath, fileType.FilePath);
            var files = InitDirectory(Path.Combine(AppPathInfo.InputPath, fileType.FilePath))
                .GetFiles(fileType.Type, SearchOption.TopDirectoryOnly);
            _hashFileMap = InitDirectory(hashDir)
                .GetFiles($"{fileType.Type}.hashfile", SearchOption.TopDirectoryOnly)
                .ToDictionary(info => Path.GetFileNameWithoutExtension(info.Name), info => info);
            using var crypto = SHA1.Create();
            _changeDetector = new ChangeDetector(crypto);

            try
            {
                await Task.WhenAll(files.Select(file => BuildFileAsync(fileType, hashDir, crypto, file))).ConfigureAwait(false);
                await fileType.CleanAsync(_hashFileMap.Keys).ConfigureAwait(false);
            }
            finally
            {
                crypto.Clear();
            }
        }

        private async Task BuildFileAsync(FileType fileType, string hashDir, HashAlgorithm crypto, IFileInfo file)
        {
            Stream fileStream = file.Open(FileMode.Open, FileAccess.Read);
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
                    hashStream = _fileSystem.File.Create($"{hashDir}{file.Name}.hashfile");
                    hash = crypto.ComputeHash(fileStream);
                }
                if (hash != null)
                {
                    await fileType.SaveAsync(file, fileStream).ConfigureAwait(false);
                    await _changeDetector.WriteHashAsync(hash, hashStream).ConfigureAwait(false);
                }
            }
            finally
            {
                fileStream.Close();
                hashStream?.Close();
            }
        }
    }
}
