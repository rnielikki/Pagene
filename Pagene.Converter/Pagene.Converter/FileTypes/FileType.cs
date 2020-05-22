using System.IO.Abstractions;
using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pagene.Converter.FileTypes
{
    internal abstract class FileType
    {
        internal string FilePath { get; private set; }

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
            string targetPath = System.IO.Path.Combine(FilePath, info.Name);
            Stream writeTarget = _fileSystem.File.Open(targetPath, FileMode.OpenOrCreate);
            try
            {
                await fileStream.CopyToAsync(writeTarget);
            }
            finally
            {
                writeTarget.Close();
            }
        }
    }
}
