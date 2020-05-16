using System.IO.Abstractions;
using System.IO;

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

        internal virtual async System.Threading.Tasks.Task Save(IFileInfo info, Stream fileStream)
        {
            fileStream.Position = 0;
            Stream writeTarget = _fileSystem.File.Open(System.IO.Path.Combine(Path, info.Name), FileMode.OpenOrCreate);
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
