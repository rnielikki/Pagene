using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Pagene.Models;

namespace Pagene.Converter
{
    internal class Formatter : IFormatter
    {
        private readonly string _path;
        internal Formatter(string path)
        {
            _path = path;
        }
        async System.Threading.Tasks.Task<(IEnumerable<string>, BlogEntry)> IFormatter.GetBlogHead(IFileInfo info, Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string rawTag = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(rawTag) || rawTag[0] != '[' || rawTag[^1] != ']')
            {
                throw new FormatException($"Couldn't find tags: The format of post {info.Name} does not match");
            }
            var tags = rawTag[1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag=>tag.Trim()).Distinct();
            string title = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(title) || !string.Equals(title.Substring(0, 2),"# "))
            {
                throw new FormatException($"Couldn't find title: The format of post {info.Name} does not match");
            }
            return (tags, new BlogEntry
            {
                Title = title.Substring(2),
                Date = info.CreationTimeUtc,
                URL = $"{_path}/{info.Name}"
            });
        }
    }
}
