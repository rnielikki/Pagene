using System.IO.Abstractions;
using System.IO;
using System.Collections.Generic;
using Pagene.BlogSettings;
using System.Threading.Tasks;

namespace Pagene.Converter.FileTypes
{
    internal abstract class FileType
    {
        internal string FilePath { get; }
        internal virtual string Type { get; }

        protected readonly IFileSystem _fileSystem;

        public virtual SearchOption DirectorySearchOption { get; protected set; } = SearchOption.AllDirectories;

        protected FileType(IFileSystem fileSystem, string path)
        {
            FilePath = path;
            _fileSystem = fileSystem;
        }

        internal virtual async Task SaveAsync(IFileInfo targetFileInfo, Stream sourceStream)
        {
            sourceStream.Position = 0;
            Stream writeTarget = GetFileStream(targetFileInfo.Name);
            try
            {
                await sourceStream.CopyToAsync(writeTarget).ConfigureAwait(false);
            }
            finally
            {
                writeTarget.Close();
            }
        }

        protected Stream GetFileStream(string fileName)
        {
            return _fileSystem.File.Open(GetOutputPath(fileName), FileMode.Create);
        }

        protected virtual string GetOutputPath(string fileName)
        {
            return Path.Combine(AppPathInfo.OutputPath, FilePath, fileName);
        }

        internal virtual Task CleanAsync(IEnumerable<string> files)
        {
           foreach (var fileName in files)
            {
                _fileSystem.File.Delete(GetOutputPath(fileName));
                _fileSystem.File.Delete(Path.Combine(AppPathInfo.BlogHashPath, FilePath, fileName+".hashfile"));
            }
            return Task.CompletedTask;
        }
    }
}
