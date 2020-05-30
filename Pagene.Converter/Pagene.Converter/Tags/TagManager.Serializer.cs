using Pagene.BlogSettings;
using Pagene.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace Pagene.Converter
{
    internal partial class TagManager
    {
        private void Deserialize()
        {
            var directory = _fileSystem.Directory.Exists(_dirName) ? _fileSystem.DirectoryInfo.FromDirectoryName(_dirName) : _fileSystem.Directory.CreateDirectory(_dirName);

            foreach (var file in directory.GetFiles("*.json", SearchOption.TopDirectoryOnly))
            {
                if (file.Name == "meta.tags.json") continue;
                using var stream = file.OpenRead();
                var tagInfo = JsonSerializer.Deserialize<TagInfo>(stream);
                string tag = tagInfo.Tag;
                var posts = tagInfo.Posts;
                if (tag == null || posts == null) continue;

                var mappedPosts = new ConcurrentDictionary<string, BlogEntry>(tagInfo.Posts.ToDictionary(info => info.Url, info => info));
                if (!_tagMap.TryAdd(tag, mappedPosts))
                {
                    new FileLoadException("Failed to load tag list", file.Name);
                }
            }
        }
        internal async System.Threading.Tasks.Task Serialize()
        {
            using var tagMeta = _fileSystem.FileInfo.FromFileName($"{_dirName}meta.tags.json").Open(FileMode.Create);
            var metaMap = new Dictionary<string, TagMeta>();
            int fileName = 0;
            foreach (var tagPair in _tagMap)
            {
                string path = $"{_dirName}{fileName++}.json";
                var item = new TagInfo { Tag = tagPair.Key, Posts = tagPair.Value.Values };
                using var file = _fileSystem.File.Open(path, FileMode.Create);
                await file.WriteAsync(JsonSerializer.Serialize(item));
                if (!metaMap.ContainsKey(tagPair.Key))
                {
                    metaMap.Add(tagPair.Key, new TagMeta { Url = path, Count = 1 });
                }
                else
                {
                    metaMap[tagPair.Key].Count++;
                }
            }
            await tagMeta.WriteAsync(JsonSerializer.Serialize(metaMap));
        }
    }
}
