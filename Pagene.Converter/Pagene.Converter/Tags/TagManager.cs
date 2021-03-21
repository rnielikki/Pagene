using Pagene.BlogSettings;
using Pagene.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    internal partial class TagManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> _tagMap;
        internal IEnumerable<string> GetTags() => _tagMap.Keys;
        private readonly IFileSystem _fileSystem;
        private readonly string _dirName = AppPathInfo.BlogTagPath;
        private readonly ConcurrentBag<string> _removedTags = new ConcurrentBag<string>();
        internal IEnumerable<string> GetRemovedTags() => _removedTags.AsEnumerable();
        internal TagManager(IFileSystem fileSystem)
        {
            _tagMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>(StringComparer.OrdinalIgnoreCase);
            _fileSystem = fileSystem;
            Deserialize();
        }
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
        internal void RemoveTag(IEnumerable<string> tags, string Url)
        {
            foreach (string tag in tags)
            {
                var targetTagItems = _tagMap[tag];
                targetTagItems.Remove(Url, out _);
                if (targetTagItems.IsEmpty)
                {
                    _tagMap.Remove(tag, out _);
                    _removedTags.Add(tag);
                }
            }
        }
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
