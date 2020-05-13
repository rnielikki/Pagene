using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Converter.Tests")]
namespace Pagene.Converter
{
    internal class ChangeDetector
    {
        private readonly IFileSystem _fileSystem;
        private readonly Dictionary<string, IFileInfo> _mdFiles;
        private readonly Dictionary<string, IFileInfo> _hashFiles;
        private const string _hashDirName = ".hash";
        internal ChangeDetector(IFileSystem fileSystem, string sourcePath)
        {
            _fileSystem = fileSystem;
            _mdFiles = InitDirectory(fileSystem, sourcePath)
                .GetFiles("*.md", System.IO.SearchOption.TopDirectoryOnly).ToDictionary(info => System.IO.Path.GetFileName(info.Name), info => info);
            _hashFiles = InitDirectory(fileSystem, _hashDirName)
                .GetFiles("*.md.hashfile", System.IO.SearchOption.TopDirectoryOnly)
                .ToDictionary(info => System.IO.Path.GetFileNameWithoutExtension(info.Name), info => info);
        }
        private IDirectoryInfo InitDirectory(IFileSystem fileSystem, string path)
        {
            var info = fileSystem.DirectoryInfo.FromDirectoryName(path);
            if (!info.Exists)
            {
                return fileSystem.Directory.CreateDirectory(path);
            }
            else
            {
                return info;
            }
        }
        internal ChangeDetector(string sourcePath) : this(fileSystem: new FileSystem(), sourcePath)
        {
        }
        internal async IAsyncEnumerable<IFileInfo> DetectAsync()
        {
            using var sha1 = SHA1.Create();
            CleanHashAsync();

            foreach (var file in _mdFiles)
            {
                if (!await DetectFileAsync(sha1, file.Key))
                {
                    yield return file.Value;
                }
            }
        }
        private async Task<bool> DetectFileAsync(HashAlgorithm crypto, string filename)
        {
            using var targetContent = _mdFiles[filename].OpenRead();
            var hash = crypto.ComputeHash(targetContent);

            bool ifHashExists = _hashFiles.ContainsKey(filename);
            using var hashStream = (ifHashExists?_hashFiles[filename].Open(System.IO.FileMode.Open):_fileSystem.File.Create($"{_hashDirName}\\{filename}.hashfile"));
            //hashStream.Seek(0, System.IO.SeekOrigin.Begin);
            if (ifHashExists)
            {
                byte[] buffer = new byte[hash.Length];
                await hashStream.ReadAsync(buffer, 0, buffer.Length);
                if (hash.SequenceEqual(buffer))
                {
                    return true; // same
                }
            }
            hashStream.Seek(0, System.IO.SeekOrigin.Begin);
            await hashStream.WriteAsync(hash, 0, hash.Length);
            return false; // not same
        }
        private void CleanHashAsync()
        {
            var cleanTargets = _hashFiles.Keys.Except(_mdFiles.Keys);
            foreach (string target in cleanTargets)
            {
                _hashFiles[target].Delete();
            }
        }
    }
}
