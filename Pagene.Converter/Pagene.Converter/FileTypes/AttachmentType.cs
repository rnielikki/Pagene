using System;
using System.IO;
using System.IO.Abstractions;

namespace Pagene.Converter.FileTypes
{
    class AttachmentType : FileType
    {
        internal override string Path => "contents/files";

        internal override string Type => "*";
        internal AttachmentType(IFileSystem fileSystem):base(fileSystem)
        {
        }

        internal override async System.Threading.Tasks.Task Save(IFileInfo info, Stream fileStream)
        {
            await base.Save(info, fileStream);
        }
    }
}
