using Pagene.BlogSettings;
using Pagene.Models;
using System.IO.Abstractions;
using System.Linq;
using Utf8Json;

namespace Pagene.Converter
{
    /// <summary>
    /// Detects and creates JSON files about recent posts.
    /// </summary>
    /// <remarks>This uses file creation and modification date, which can be changed manually.</remarks>
    internal class RecentPostManager
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFormatter _formatter;
        private readonly string fileName = AppPathInfo.BlogEntryPath + "recent.json";

        /// <summary>
        /// Contructor for detecting and creating JSON files about recent posts.
        /// </summary>
        /// <param name="fileSystem">The file system that be used, which can be mocked or not.</param>
        /// <param name="formatter">Formatter to get blog entry.</param>
        internal RecentPostManager(IFileSystem fileSystem, IFormatter formatter)
        {
            _fileSystem = fileSystem;
            _formatter = formatter;
        }

        /// <summary>
        /// Gets certain number of recent blog entrys, by creation date (fresh first).
        /// </summary>
        /// <param name="count">How many amount of post entries wanted.</param>
        internal async System.Threading.Tasks.Task<BlogEntry[]> GetRecentPosts(int count)
        {
            var files = _fileSystem.DirectoryInfo.FromDirectoryName(System.IO.Path.Combine(AppPathInfo.InputPath, AppPathInfo.ContentPath))
                .GetFiles("*.md", System.IO.SearchOption.TopDirectoryOnly);
            var orderedFiles = files.OrderByDescending(file => file.CreationTime).Take(count);
            return await System.Threading.Tasks.Task.WhenAll(
                    orderedFiles.Select(file =>_formatter.GetBlogEntryAsync(file))
                ).ConfigureAwait(false);
        }

        /// <summary>
        /// Save blog entries to JSON files.
        /// </summary>
        /// <param name="entries">The blog entries to write to recent JSON file.</param>
        /// <remarks>This method itself doesn't create order.</remarks>
        internal async System.Threading.Tasks.Task Serialize(BlogEntry[] entries)
        {
            using var fileStream = _fileSystem.File.Open(fileName, System.IO.FileMode.Create);
            await JsonSerializer.SerializeAsync(fileStream, entries).ConfigureAwait(false);
        }
    }
}
