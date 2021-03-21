using Pagene.BlogSettings;
using System.IO.Abstractions;

namespace Pagene.Converter.FileTypes
{
    internal class AttachmentFileType : FileType
    {
        internal override string Type => "*";
        internal AttachmentFileType(IFileSystem fileSystem) : base(fileSystem, AppPathInfo.FilePath)
        {
        }
    }
}
