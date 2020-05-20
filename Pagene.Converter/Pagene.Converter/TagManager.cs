using Pagene.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    partial class TagManager
    {
        //sometimes can be replaced to ConcurrentDictionary
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> _tagMap;
        private readonly IFileSystem _fileSystem;
        private const string _dirName = "tags";
        internal TagManager(IFileSystem fileSystem)
        {
            _tagMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>(StringComparer.OrdinalIgnoreCase);
            _fileSystem = fileSystem;
            Deserialize();
        }
        internal void AddTag(IEnumerable<string> tags, BlogEntry entry)
        {
            foreach (string tag in tags)
            {
                if (tag == "meta.tags")
                {
                    throw new InvalidDataException("Reserved name \"meta.tags\" cannot be used as tag.");
                }
                var existEntries = _tagMap.GetOrAdd(tag, new ConcurrentDictionary<string, BlogEntry>());
                existEntries.AddOrUpdate(Path.GetFileName(entry.URL), entry,(k, v) => v = entry);
            }
        }
        internal void CleanTags(IEnumerable<string> targetPaths)
        {
            if (!targetPaths.Any()) return;
            foreach (var tagEntries in _tagMap)
            {
                var currentEntries = tagEntries.Value;
                var clearPaths = currentEntries.Keys.Intersect(targetPaths);
                if (clearPaths.Any())
                {
                    foreach (var path in clearPaths)
                    {
                        currentEntries.Remove(path, out _);
                    }
                    if (!currentEntries.Any())
                    {
                        _tagMap.Remove(tagEntries.Key, out _);
                        _fileSystem.File.Delete($"{_dirName}/{tagEntries.Key}.json");
                    }
                }
            }
        }
    }
}
