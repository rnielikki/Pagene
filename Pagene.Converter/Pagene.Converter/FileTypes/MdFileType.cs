using System.IO;
using System.IO.Abstractions;

namespace Pagene.Converter.FileTypes
{
    class MdFileType : FileType
    {
        internal override string Path => "contents";

        internal override string Type => "*.md";
        private readonly Formatter formatter = new Formatter();
        internal MdFileType(IFileSystem fileSystem):base(fileSystem)
        {
        }

        internal override async System.Threading.Tasks.Task Save(IFileInfo info, Stream fileStream)
        {
            using Stream stream = new MemoryStream();
            using StreamReader reader = new StreamReader(fileStream);
            using StreamWriter writer = new StreamWriter(stream);
            await formatter.GetBlogHead(info, fileStream); // validation

            writer.WriteLine(@$"> {info.CreationTimeUtc}");
            writer.WriteLine(@$"> {info.LastWriteTimeUtc}");
            writer.Flush();
            fileStream.CopyTo(stream);

            //generate categories

            //save
            await base.Save(info, stream);
        }
    }
}
