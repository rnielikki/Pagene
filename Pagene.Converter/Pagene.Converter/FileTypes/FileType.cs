using System.IO.Abstractions;
using System.IO;
using System.Collections.Generic;
using Pagene.BlogSettings;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pagene.Converter.FileTypes
{
    internal abstract class FileType
    {
        internal string FilePath { get; }
        internal virtual string Type { get; }
        internal virtual string OutputType { get; }

        protected readonly IFileSystem _fileSystem;
        internal FileType(IFileSystem fileSystem, string path)
        {
            FilePath = path;
            _fileSystem = fileSystem;
        }

        internal virtual async Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            fileStream.Position = 0;
            Stream writeTarget = GetFileStream(info.Name);
            try
            {
                await fileStream.CopyToAsync(writeTarget).ConfigureAwait(false);
            }
            finally
            {
                writeTarget.Close();
            }
        }
        protected Stream GetFileStream(string fileName)
        {
            return _fileSystem.File.Open(GetOutputPath(fileName), FileMode.OpenOrCreate);
        }

        protected string GetOutputPath(string fileName)
        {
            return Path.Combine(AppPathInfo.OutputPath, FilePath, Path.GetFileNameWithoutExtension(fileName)+OutputType);
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
