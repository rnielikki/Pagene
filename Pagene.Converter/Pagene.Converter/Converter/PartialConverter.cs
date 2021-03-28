using Pagene.BlogSettings;
using Pagene.Converter.FileTypes;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Pagene.Converter
{
    /// <summary>
    /// The real converting part for one <see cref="FileType"/>. Used by <see cref="Converter.BuildAsync()">.
    /// </summary>
    internal class PartialConverter
    {
        private readonly FileType _fileType;
        private readonly IFileSystem _fileSystem;
        private ChangeDetector _changeDetector;
        private ConcurrentDictionary<string, IFileInfo> _hashFileMap;
        private string _filePath;

        /// <summary>
        /// The real converting part for one <see cref="FileType"/>. Used by <see cref="Converter.BuildAsync()">.
        /// </summary>
        /// <param name="fileType">The 'collection of converting type definition' class.</param>
        /// <param name="fileSystem">The file system used for converting, can be mocked or not.</param>
        internal PartialConverter(FileType fileType, IFileSystem fileSystem)
        {
            _fileType = fileType;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Starts the file converting process, for one <see cref="FileType"/> unit.
        /// </summary>
        internal async Task BuildAsync()
        {
            _filePath = Path.Combine(AppPathInfo.InputPath, _fileType.FilePath);
            var files = InitializationHelper.InitDirectory(_fileSystem, _filePath)
                .GetFiles(_fileType.Extension, _fileType.DirectorySearchOption);

            string hashDir = Path.Combine(AppPathInfo.BlogHashPath, _fileType.FilePath);
            _hashFileMap = new (
                InitializationHelper.InitDirectory(_fileSystem, hashDir)
                .GetFiles($"{_fileType.Extension}.hashfile", _fileType.DirectorySearchOption)
                .ToDictionary(info => info.GetRelativeName(hashDir, _fileType.DirectorySearchOption)[0..^9], info => info)
            );

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
                string fileName = file.GetRelativeName(_filePath, _fileType.DirectorySearchOption);
                if (_hashFileMap.TryGetValue(fileName, out IFileInfo hashFile))
                {
                    hashStream = hashFile.Open(FileMode.OpenOrCreate);
                    hash = await _changeDetector.DetectAsync(fileStream, hashStream).ConfigureAwait(false);
                    _hashFileMap.TryRemove(fileName, out _);
                }
                else
                {
                    //new hash
                    _fileSystem.Directory.CreateDirectoriesIfNotExist(hashDir, fileName);
                    hashStream = _fileSystem.File.Create($"{Path.Combine(hashDir, fileName)}.hashfile");
                    hash = crypto.ComputeHash(fileStream);
                }
                if (hash != null)
                {
                    await _fileType.SaveAsync(file, fileStream).ConfigureAwait(false);
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
