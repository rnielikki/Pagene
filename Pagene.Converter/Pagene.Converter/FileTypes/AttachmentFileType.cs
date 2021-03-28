using Pagene.BlogSettings;
using System.IO.Abstractions;

namespace Pagene.Converter.FileTypes
{
    /// <inheritdoc cref="FileType"/>
    internal class AttachmentFileType : FileType
    {
        /// <summary>
        /// Constructor the converting type defintiion class.
        /// </summary>
        internal override string Extension => "*";
        internal AttachmentFileType(IFileSystem fileSystem) : base(fileSystem, AppPathInfo.FilePath)
        {
        }
    }
}
