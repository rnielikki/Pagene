using Pagene.BlogSettings;
using Pagene.Converter.FileTypes;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Pagene.Converter
{
    internal class PartialConverter
    {
        private readonly FileType _fileType;
        private readonly IFileSystem _fileSystem;
        private ChangeDetector _changeDetector;
        private Dictionary<string, IFileInfo> _hashFileMap;

        internal PartialConverter(FileType fileType, IFileSystem fileSystem)
        {
            _fileType = fileType;
            _fileSystem = fileSystem;
        }
        internal async Task BuildAsync()
        {
            var files = InitializationHelper.InitDirectory(_fileSystem, Path.Combine(AppPathInfo.InputPath, _fileType.FilePath))
                .GetFiles(_fileType.Type, _fileType.DirectorySearchOption);

            string hashDir = Path.Combine(AppPathInfo.BlogHashPath, _fileType.FilePath);
            _hashFileMap = InitializationHelper.InitDirectory(_fileSystem, hashDir)
                .GetFiles($"{_fileType.Type}.hashfile", _fileType.DirectorySearchOption)
                .ToDictionary(info => info.Name[0..^9], info => info);

            using var crypto = SHA1.Create();
            _changeDetector = new ChangeDetector(crypto);

            try
            {
                await Task.WhenAll(files.Select(file => BuildFileAsync(hashDir, crypto, file))).ConfigureAwait(false);

                //Cleans leftover files - This exists, because hash of removed files shouldn't be there.
                await _fileType.CleanAsync(_hashFileMap.Keys).ConfigureAwait(false);
            }
            finally
            {
                crypto.Clear();
            }
        }

        private async Task BuildFileAsync(string hashDir, HashAlgorithm crypto, IFileInfo file)
        {
            Stream fileStream = file.Open(FileMode.Open, FileAccess.Read);
            Stream hashStream = null;
            byte[] hash;
            try
            {
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
                    await _fileType.SaveAsync(file, fileStream).ConfigureAwait(false);
                    await ChangeDetector.WriteHashAsync(hash, hashStream).ConfigureAwait(false);
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
