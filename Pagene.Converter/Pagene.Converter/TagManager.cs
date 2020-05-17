using Pagene.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace Pagene.Converter
{
    class TagManager:IDisposable
    {
        //sometimes can be replaced to ConcurrentDictionary
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> _tagMap;
        private readonly Stream fileStream;
        internal TagManager(System.IO.Abstractions.IFileSystem fileSystem)
        {
            fileStream = fileSystem.File.Open("meta.tags.json", FileMode.OpenOrCreate);
            if (fileStream.Length != 0)
            {
                var deserialized = JsonSerializer.Deserialize<Dictionary<string, List<BlogEntry>>>(fileStream);

                var mapped = deserialized.Select(pair =>
                    new KeyValuePair<string, ConcurrentDictionary<string, BlogEntry>>
                    (pair.Key, new ConcurrentDictionary<string, BlogEntry>(pair.Value.ToDictionary(item => item.URL, item=> item))));

                _tagMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>(mapped);
            }
            else
            {
                _tagMap = new ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>();
            }
        }
        internal void AddTag(IEnumerable<string> tags, BlogEntry entry)
        {
            foreach (string tag in tags)
            {
                var existEntries = _tagMap.GetOrAdd(tag, new ConcurrentDictionary<string, BlogEntry>());
                existEntries.AddOrUpdate(entry.URL, entry,(k, v) => v = entry);
            }
        }
        internal async System.Threading.Tasks.Task Serialize()
        {
            
            var MapForWriting =_tagMap.ToDictionary(kv => kv.Key, kv => kv.Value.Values);

            StreamWriter writer = new StreamWriter(fileStream);
            fileStream.Position = 0;
            await writer.WriteAsync(JsonSerializer.ToJsonString(MapForWriting));
            writer.Flush();
        }
        public void Dispose()
        {
            fileStream.Close();
        }

        internal void CleanTags(IEnumerable<string> removedTarget)
        {
            var targetPaths = removedTarget.Select(item => $"contents/{item}");
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
                    }
                }
            }
        }
    }
}
