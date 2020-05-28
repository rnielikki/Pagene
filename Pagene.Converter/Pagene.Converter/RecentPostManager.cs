using Pagene.BlogSettings;
using Pagene.Models;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    internal class RecentPostManager
    {
        private IFileSystem _fileSystem;
        private IFormatter _formatter;

        internal RecentPostManager(IFileSystem fileSystem, IFormatter formatter)
        {
            _fileSystem = fileSystem;
            _formatter = formatter;
        }
        internal async System.Threading.Tasks.Task<BlogEntry[]> GetRecentPosts(int count)
        {
            var files = _fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogInputPath)
                .GetFiles("*.md", System.IO.SearchOption.TopDirectoryOnly);
            var orderedFiles = files.OrderByDescending(file => file.CreationTimeUtc).Take(count);
            var tasks = System.Threading.Tasks.Task.WhenAll(
                    orderedFiles.Select(file =>_formatter.GetBlogHead(file))
                );
            return await tasks.ConfigureAwait(false);
        }
    }
}
