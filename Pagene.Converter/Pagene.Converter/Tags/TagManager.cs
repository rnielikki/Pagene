using Pagene.BlogSettings;
using Pagene.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    /// <summary>
    /// Represents the tag creating and managing logics.
    /// </summary>
    internal partial class TagManager
    {
        // ConcurrentDirectory<Tag, ConcurrentDirectory<BlogUrl, BlogEntry>>
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> _tagMap;

        /// <summary>
        /// Gets the tags that have been found from the posts.
        /// </summary>
        internal IEnumerable<string> GetTags() => _tagMap.Keys;
        private readonly IFileSystem _fileSystem;
        private readonly string _dirName = AppPathInfo.BlogTagPath;
        private readonly ConcurrentBag<string> _removedTags = new();

        /// <summary>
        /// Get tags that doesn't exist in posts anymore, for cleaning purpose.
        /// </summary>
        internal IEnumerable<string> GetRemovedTags() => _removedTags.AsEnumerable();

        /// <summary>
        /// Constructor of tag managing logic class.
        /// </summary>
        /// <param name="fileSystem">The file system, which can be mocked or not.</param>
        internal TagManager(IFileSystem fileSystem)
        {
            _tagMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>(StringComparer.OrdinalIgnoreCase);
            _fileSystem = fileSystem;
            Deserialize();
        }

        /// <summary>
        /// Add or update tag to blog entry and tag map, without duplication.
        /// </summary>
        /// <param name="tags">The tags to add.</param>
        /// <param name="entry">The <see cref="BlogEntry"/> to apply tags.</param>
        internal void AddTag(IEnumerable<string> tags, BlogEntry entry)
        {
            var oldTags = SearchTagsByEntry(entry.Url);
            //should be added
            foreach (string tag in tags.Except(oldTags))
            {
                var existEntries = _tagMap.GetOrAdd(tag, new ConcurrentDictionary<string, BlogEntry>());
                existEntries.AddOrUpdate(entry.Url, entry, (_, v) => v = entry);
            }
            //should be removed
            RemoveTag(oldTags.Except(tags), entry.Url);
        }
        private IEnumerable<string> SearchTagsByEntry(string url) => _tagMap.Where(kv => kv.Value.ContainsKey(url)).Select(kv => kv.Key);

        /// <summary>
        /// Removes tags from specific blog post Url.
        /// </summary>
        /// <param name="tags">The tags to remove.</param>
        /// <param name="url">The Blog Url (unique value) to remove tags.</param>
        internal void RemoveTag(IEnumerable<string> tags, string url)
        {
            foreach (string tag in tags)
            {
                var targetTagItems = _tagMap[tag];
                targetTagItems.Remove(url, out _);
                if (targetTagItems.IsEmpty)
                {
                    _tagMap.Remove(tag, out _);
                    _removedTags.Add(tag);
                }
            }
        }

        /// <summary>
        /// Clean tags from the file that doesn't exist. It removes the entire tag, if the tag is not used anymore.
        /// </summary>
        /// <param name="fileList">The list of file that was already deleted.</param>
        internal void Clean(IEnumerable<string> fileList)
        {
            CleanFromDeletedFile(fileList);
            CleanTags();
        }
        private void CleanFromDeletedFile(IEnumerable<string> fileList)
        {
            foreach (string file in fileList)
            {
                var url = System.IO.Path.Combine(RoutePathInfo.ContentPath, System.IO.Path.GetFileNameWithoutExtension(file)).Replace('\\', '/');
                var tags = SearchTagsByEntry(url);
                RemoveTag(tags, url);
            }
        }
        private void CleanTags()
        {
            var cleanTargetTags = GetRemovedTags();
            if (!cleanTargetTags.Any()) return;
            foreach (var targetTag in cleanTargetTags)
            {
                _fileSystem.File.Delete($"{AppPathInfo.BlogTagPath}{targetTag.ToLower()}.json");
            }
        }
    }
}
