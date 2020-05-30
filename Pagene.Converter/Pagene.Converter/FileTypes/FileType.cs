using System.IO.Abstractions;
using System.IO;
using System.Collections.Generic;
using Pagene.BlogSettings;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pagene.Converter.FileTypes
{
    internal abstract class FileType
    {
        internal string FilePath { get; }

        internal virtual string Type { get; }

        protected readonly IDirectoryInfo Directory;
        protected readonly IFileSystem _fileSystem;
        internal FileType(IFileSystem fileSystem, string path)
        {
            FilePath = Path.Combine(Converter.RealPath, path);
            _fileSystem = fileSystem;
            if (!fileSystem.Directory.Exists(FilePath))
            {
                Directory = fileSystem.Directory.CreateDirectory(FilePath);
            }
            else
            {
                Directory = fileSystem.DirectoryInfo.FromDirectoryName(FilePath);
            }
        }

        internal virtual async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            fileStream.Position = 0;
            string targetPath = Path.Combine(FilePath, info.Name);
            Stream writeTarget = _fileSystem.File.Open(targetPath, FileMode.OpenOrCreate);
            try
            {
                await fileStream.CopyToAsync(writeTarget).ConfigureAwait(false);
            }
            finally
            {
                writeTarget.Close();
            }
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        internal virtual async System.Threading.Tasks.Task Clean(IEnumerable<string> files)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
           foreach (var fileName in files)
            {
                _fileSystem.File.Delete(Path.Combine(FilePath, fileName));
                _fileSystem.File.Delete(Path.Combine(AppPathInfo.HashPath, FilePath, fileName+".hashfile"));
            }
        }
    }
}
