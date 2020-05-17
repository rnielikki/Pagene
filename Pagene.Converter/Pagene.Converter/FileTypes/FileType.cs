using System.IO.Abstractions;
using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pagene.Converter.FileTypes
{
    internal abstract class FileType
    {
        internal virtual string Path { get; }

        internal virtual string Type { get; }

        protected readonly IDirectoryInfo Directory;
        protected readonly IFileSystem _fileSystem;
        internal FileType(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            if (!fileSystem.Directory.Exists(Path))
            {
                Directory = fileSystem.Directory.CreateDirectory(Path);
            }
            else
            {
                Directory = fileSystem.DirectoryInfo.FromDirectoryName(Path);
            }
        }

        internal virtual async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            fileStream.Position = 0;
            string targetPath = System.IO.Path.Combine(Path, info.Name);
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
