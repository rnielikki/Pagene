using Pagene.Models;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Pagene.BlogSettings;

namespace Pagene.Converter.FileTypes
{
    internal class MdFileType : FileType
    {
        internal override string Type => "*.md";
        private readonly IFormatter _formatter;
        private readonly TagManager _tagManager;
        private bool modified;
        internal MdFileType(IFileSystem fileSystem, IFormatter formatter, TagManager tagManager):base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = formatter;
            _tagManager = tagManager;
        }

        internal MdFileType(IFileSystem fileSystem, TagManager tagManager):base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = new Formatter(AppPathInfo.ContentPath);
            _tagManager = tagManager;
        }

        internal override async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            modified = true;
            using Stream stream = new MemoryStream();
            using StreamReader reader = new StreamReader(fileStream);
            using StreamWriter writer = new StreamWriter(stream);
            fileStream.Position = 0;
            BlogEntry entry = await _formatter.GetBlogHead(info, fileStream).ConfigureAwait(false);

            writer.WriteLine($"> {entry.Date}");
            writer.WriteLine($"> {info.LastWriteTimeUtc}");
            writer.Flush();
            fileStream.Position = 0;
            fileStream.CopyTo(stream);

            //generate categories
            _tagManager.AddTag(entry.Tags, entry);

            //save
            await base.SaveAsync(info, stream).ConfigureAwait(false);
        }
        internal override async System.Threading.Tasks.Task Clean(IEnumerable<string> files)
        {
            if (!modified) return;

            await base.Clean(files).ConfigureAwait(false);
            _tagManager.CleanFromDeletedFile(files);
            _cleaner.CleanTags(_tagManager);

            await _tagManager.Serialize().ConfigureAwait(false);
            var postManager = new RecentPostManager(_fileSystem, _formatter);
            await postManager.Serialize(await postManager.GetRecentPosts(20).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}
