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
    public class Converter
    {
        private readonly IFileSystem _fileSystem;
        private ChangeDetector _changeDetector;
        private Dictionary<string, IFileInfo> _hashFileMap;
        internal Converter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public Converter():this(new FileSystem()){}

        public async Task Convert()
        {
            await Task.WhenAll(ConvertPart(new MdFileType(_fileSystem)), ConvertPart(new AttachmentType(_fileSystem)));
        }
        private async Task ConvertPart(FileType fileType)
        {
            string dir = fileType.Path;
            string hashDir = $".hash\\{dir}";
            var files = InitDirectory("inputs\\"+dir)
                .GetFiles(fileType.Type, SearchOption.TopDirectoryOnly);
            _hashFileMap = InitDirectory(hashDir)
                .GetFiles($"{fileType.Type}.hashfile", SearchOption.TopDirectoryOnly)
                .ToDictionary(info => Path.GetFileNameWithoutExtension(info.Name), info => info);
            using var crypto = SHA1.Create();
            _changeDetector = new ChangeDetector(crypto);
            try
            {
                foreach (var file in files)
                {
                    await ConvertFile(fileType, hashDir, crypto, file);
                }
                _changeDetector.CleanHashAsync(_hashFileMap.Values);
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
                    hash = await _changeDetector.DetectAsync(fileStream, hashStream);
                    _hashFileMap.Remove(file.Name);
                }
                else
                {
                    hashStream = File.Create($"{hashDir}\\{file.Name}.hashfile");
                    hash = crypto.ComputeHash(fileStream);
                }
                if (hash != null)
                {
                    await fileType.Save(file, fileStream);
                    await _changeDetector.WriteHash(hash, hashStream);
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
