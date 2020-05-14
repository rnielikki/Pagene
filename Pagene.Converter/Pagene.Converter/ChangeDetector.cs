using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Converter.Tests")]
namespace Pagene.Converter
{
    internal class ChangeDetector
    {
        private readonly DirSettings _setting;
        private readonly IFileSystem _fileSystem;
        //private readonly Dictionary<string, IFileInfo> _mdFiles;
        //private readonly Dictionary<string, IFileInfo> _hashFiles;
        private readonly Dictionary<string, IFileInfo> _attachFiles;
        internal ChangeDetector(IFileSystem fileSystem, DirSettings setting)
        {
            _setting = setting;
            _fileSystem = fileSystem;
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
        internal ChangeDetector(IConfiguration configuration) : this(new FileSystem(), new DirSettings(configuration))
        {
        }
        internal IAsyncEnumerable<IFileInfo> DetectBlogPostAsync() => DetectAsync("*.md", _setting.ContentDir);
        internal IAsyncEnumerable<IFileInfo> DetectAttachmentsAsync() => DetectAsync("*", $"{_setting.ContentDir}\\{_setting.AttachmentDir}");
        private async IAsyncEnumerable<IFileInfo> DetectAsync(string fileType, string dir)
        {
            var files = InitDirectory(_fileSystem, dir)
                .GetFiles(fileType, System.IO.SearchOption.TopDirectoryOnly).ToDictionary(info => System.IO.Path.GetFileName(info.Name), info => info);
            var hashFiles = InitDirectory(_fileSystem, $"{_setting.HashDir}\\{dir}")
                .GetFiles($"{fileType}.hashfile", System.IO.SearchOption.TopDirectoryOnly).ToDictionary(info => System.IO.Path.GetFileNameWithoutExtension(info.Name), info => info);

            using var sha1 = SHA1.Create();
            CleanHashAsync(files, hashFiles);

            foreach (var file in files)
            {
                if (!await DetectFileAsync(files, hashFiles, dir, file.Key, sha1))
                {
                    yield return file.Value;
                }
            }
        }
        //FIXME: if the program ends after this and before result some files may not converted at all
        private async Task<bool> DetectFileAsync(Dictionary<string, IFileInfo> files, Dictionary<string, IFileInfo> hashes, string dir, string filename, HashAlgorithm crypto)
        {
            string hashDir = $"{_setting.HashDir}\\{dir}";
            using var targetContent = files[filename].OpenRead();
            var hash = crypto.ComputeHash(targetContent);

            bool ifHashExists = hashes.ContainsKey(filename);
            using var hashStream = (ifHashExists?hashes[filename].Open(System.IO.FileMode.Open):_fileSystem.File.Create($"{hashDir}\\{filename}.hashfile"));
            if (ifHashExists)
            {
                byte[] buffer = new byte[hash.Length];
                await hashStream.ReadAsync(buffer, 0, buffer.Length);
                if (buffer.Length == hash.Length && hash.SequenceEqual(buffer))
                {
                    return true; // same
                }
            }
            hashStream.Seek(0, System.IO.SeekOrigin.Begin);
            await hashStream.WriteAsync(hash, 0, hash.Length);
            return false; // not same
        }
        private void CleanHashAsync(Dictionary<string, IFileInfo> files, Dictionary<string, IFileInfo> hashes)
        {
            var cleanTargets = hashes.Keys.Except(files.Keys);
            foreach (string target in cleanTargets)
            {
                hashes[target].Delete();
            }
        }
    }
}
