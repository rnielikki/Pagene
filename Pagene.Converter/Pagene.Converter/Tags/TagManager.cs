﻿using Pagene.BlogSettings;
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
        //sometimes can be replaced to ConcurrentDictionary
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
            var oldTags = SearchTagsByEntry(entry);
            //should be added
            foreach (string tag in tags.Except(oldTags))
            {
                var existEntries = _tagMap.GetOrAdd(tag, new ConcurrentDictionary<string, BlogEntry>());
                existEntries.AddOrUpdate(entry.Url, entry, (_, v) => v = entry);
            }
            //should be removed
            RemoveTag(oldTags.Except(tags), entry);
        }
        private IEnumerable<string> SearchTagsByEntry(BlogEntry entry) => _tagMap.Where(kv => kv.Value.ContainsKey(entry.Url)).Select(kv => kv.Key);
        internal void RemoveTag(IEnumerable<string> tags, BlogEntry entry)
        {
            foreach (string tag in tags)
            {
                var targetTagItems = _tagMap[tag];
                targetTagItems.Remove(entry.Url, out _);
                if (targetTagItems.Count == 0)
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
                var entry = new BlogEntry { Url = AppPathInfo.ContentPath+file };
                var tags = SearchTagsByEntry(entry);
                RemoveTag(tags, entry);
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
