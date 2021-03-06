﻿using Pagene.BlogSettings;
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
        /// <summary>
        /// Read all tags, reads tags and create tag map for use.
        /// </summary>
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
                    throw new FileLoadException("Failed to load tag list", file.Name);
                }
            }
        }

        /// <summary>
        /// Create tag maps JSON file from tag map. This should be called after converting process.
        /// </summary>
        internal async System.Threading.Tasks.Task Serialize()
        {
            using var tagMeta = _fileSystem.FileInfo.FromFileName($"{_dirName}meta.tags.json").Open(FileMode.Create);
            var metaMap = new Dictionary<string, TagMeta>();
            int fileName = 0;
            foreach (var tagPair in _tagMap)
            {
                string path = $"{_dirName}{fileName}.json";
                var item = new TagInfo { Tag = tagPair.Key, Posts = tagPair.Value.Values.OrderByDescending(post => post.Date) };
                using var file = _fileSystem.File.Open(path, FileMode.Create);
                await file.WriteAsync(JsonSerializer.Serialize(item)).ConfigureAwait(false);
                metaMap.Add(tagPair.Key, new TagMeta { Url = Path.Combine(RoutePathInfo.TagPath, fileName.ToString()).Replace('\\', '/'), Count = tagPair.Value.Count });
                fileName++;
            }
            await tagMeta.WriteAsync(JsonSerializer.Serialize(metaMap)).ConfigureAwait(false);
        }
    }
}
