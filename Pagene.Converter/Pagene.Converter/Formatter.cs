using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pagene.Models;

namespace Pagene.Converter
{
    internal class Formatter
    {
        //reader part
        /*
        internal async System.Threading.Tasks.Task<BlogItem> ParseAsync(System.IO.Abstractions.IFileInfo fileInfo, Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            var blogHeads = await GetBlogHead(reader);
            var content = await reader.ReadToEndAsync();
            return new BlogItem(
                title: blogHeads.Item1,
                content: content,
                creationDate: fileInfo.CreationTimeUtc,
                modificationDate: fileInfo.LastWriteTimeUtc,
                tags: blogHeads.Item2
                );
        }
        */
        internal async System.Threading.Tasks.Task<(IEnumerable<string>, BlogEntry)> GetBlogHead(System.IO.Abstractions.IFileInfo info, Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string rawTag = await reader.ReadLineAsync();
            if (rawTag[0] != '[' || rawTag[^1] != ']')
            {
                throw new FormatException();
            }
            var tags = rawTag[1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag=>tag.Trim());
            string title = await reader.ReadLineAsync();
            if (!string.Equals(title.Substring(0, 2),"# "))
            {
                throw new FormatException();
            }
            return (tags, new BlogEntry
            {
                Title = title,
                Date = info.CreationTimeUtc,
                URL = info.Name 
            });
        }
    }
}
