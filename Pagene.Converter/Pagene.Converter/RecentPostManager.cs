using Pagene.BlogSettings;
using Pagene.Models;
using System.IO.Abstractions;
using System.Linq;
using Utf8Json;

namespace Pagene.Converter
{
    internal class RecentPostManager
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFormatter _formatter;
        private readonly string fileName = AppPathInfo.EntryPath + "recent.json";

        internal RecentPostManager(IFileSystem fileSystem, IFormatter formatter)
        {
            _fileSystem = fileSystem;
            _formatter = formatter;
        }
        internal async System.Threading.Tasks.Task<BlogEntry[]> GetRecentPosts(int count)
        {
            var files = _fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogInputPath)
                .GetFiles("*.md", System.IO.SearchOption.TopDirectoryOnly);
            var orderedFiles = files.OrderByDescending(file => file.CreationTime).Take(count);
            return await System.Threading.Tasks.Task.WhenAll(
                    orderedFiles.Select(file =>_formatter.GetBlogEntryAsync(file))
                ).ConfigureAwait(false);
        }
        internal async System.Threading.Tasks.Task Serialize(BlogEntry[] entries)
        {
            using var fileStream = _fileSystem.File.Open(fileName, System.IO.FileMode.Create);
            await JsonSerializer.SerializeAsync(fileStream, entries).ConfigureAwait(false);
        }
    }
}
