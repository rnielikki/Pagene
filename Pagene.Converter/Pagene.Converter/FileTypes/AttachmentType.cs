using System;
using System.IO;
using System.IO.Abstractions;

namespace Pagene.Converter.FileTypes
{
    class AttachmentType : FileType
    {
        internal override string Type => "*";
        internal AttachmentType(IFileSystem fileSystem):base(fileSystem, "contents/files")
        {
        }

        internal override async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            await base.SaveAsync(info, fileStream);
        }
    }
}
