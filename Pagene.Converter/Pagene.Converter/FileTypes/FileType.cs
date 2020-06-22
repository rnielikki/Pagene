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
        internal virtual string OutputType { get; }

        protected readonly IFileSystem _fileSystem;
        internal FileType(IFileSystem fileSystem, string path)
        {
            FilePath = path;
            _fileSystem = fileSystem;
        }

        internal virtual async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
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
            string targetPath = Path.Combine(AppPathInfo.OutputPath, FilePath, ChangeExtension(fileName));
            return _fileSystem.File.Open(targetPath, FileMode.OpenOrCreate);
        }

        private string ChangeExtension(string fileName) => Path.GetFileNameWithoutExtension(fileName)+OutputType;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        internal virtual async System.Threading.Tasks.Task Clean(IEnumerable<string> files)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
           foreach (var fileName in files)
            {
                _fileSystem.File.Delete(Path.Combine(AppPathInfo.OutputPath, FilePath, ChangeExtension(fileName)));
                _fileSystem.File.Delete(Path.Combine(AppPathInfo.BlogHashPath, FilePath, fileName+".hashfile"));
            }
        }
    }
}
