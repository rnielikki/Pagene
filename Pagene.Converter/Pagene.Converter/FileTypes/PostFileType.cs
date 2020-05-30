using Pagene.Models;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Pagene.BlogSettings;
using Utf8Json;

namespace Pagene.Converter.FileTypes
{
    internal class PostFileType : FileType
    {
        internal override string Type => "*.md";
        internal override string OutputType => ".json";
        private readonly IFormatter _formatter;
        private readonly TagManager _tagManager;
        private bool modified;
        internal PostFileType(IFileSystem fileSystem, IFormatter formatter, TagManager tagManager):base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = formatter;
            _tagManager = tagManager;
        }

        internal PostFileType(IFileSystem fileSystem, TagManager tagManager):base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = new Formatter(AppPathInfo.ContentPath);
            _tagManager = tagManager;
        }

        internal override async System.Threading.Tasks.Task SaveAsync(IFileInfo info, Stream fileStream)
        {
            modified = true;
            using Stream stream = GetFileStream(info.Name);
            fileStream.Position = 0;
            using StreamReader reader = new StreamReader(fileStream);
            BlogEntry entry = await _formatter.GetBlogHead(info, reader).ConfigureAwait(false);
            BlogItem item = new BlogItem { Title = entry.Title, Content = await reader.ReadToEndAsync().ConfigureAwait(false), CreationDate = entry.Date, ModificationDate = info.LastWriteTimeUtc, Tags = entry.Tags };
            entry.Summary = _formatter.GetSummary(item.Content);
            await JsonSerializer.SerializeAsync(stream, item).ConfigureAwait(false);
            //stream.Position = 0;

            //generate categories
            _tagManager.AddTag(entry.Tags, entry);
        }
        internal override async System.Threading.Tasks.Task Clean(IEnumerable<string> files)
        {
            if (!modified) return;

            await base.Clean(files).ConfigureAwait(false);
            _tagManager.Clean(files);

            await _tagManager.Serialize().ConfigureAwait(false);
            var postManager = new RecentPostManager(_fileSystem, _formatter);
            await postManager.Serialize(await postManager.GetRecentPosts(20).ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}
