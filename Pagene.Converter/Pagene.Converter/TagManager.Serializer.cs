using Pagene.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utf8Json;

namespace Pagene.Converter
{
    partial class TagManager
    {
        private void Deserialize()
        {
            var directory = _fileSystem.Directory.Exists(_dirName) ? _fileSystem.DirectoryInfo.FromDirectoryName(_dirName) : _fileSystem.Directory.CreateDirectory(_dirName);
            var files = directory.GetFiles("*.json", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                if (file.Name == "meta.tags.json") continue;
                using var stream = file.OpenRead();
                var tagInfo = JsonSerializer.Deserialize<TagInfo>(stream);
                string tag = tagInfo.Tag;
                var posts = tagInfo.Posts;

                if (tag == null || posts == null) continue;
                var mappedPosts = new ConcurrentDictionary<string, BlogEntry>(posts.ToDictionary(post => Path.GetFileName(post.URL), post => post));

                if (!_tagMap.TryAdd(tag, mappedPosts))
                {
                    new FileLoadException("Failed to load tag list", file.Name);
                }
            }
        }
        internal async System.Threading.Tasks.Task Serialize()
        {
            using (var tagMeta = _fileSystem.FileInfo.FromFileName($"{_dirName}/meta.tags.json").Open(FileMode.Create))
            {
                var metaMap = _tagMap.ToDictionary(map => map.Key, map => map.Value.Count);
                await tagMeta.WriteAsync(JsonSerializer.Serialize(metaMap));
            }
            foreach (var tagPair in _tagMap)
            {
                var item = new TagInfo(tagPair.Key, tagPair.Value.Values);
                using var file = _fileSystem.File.Open($"{_dirName}/{item.Tag.ToLower()}.json", FileMode.Create);
                await file.WriteAsync(JsonSerializer.Serialize(item));
            }
        }
    }
}
