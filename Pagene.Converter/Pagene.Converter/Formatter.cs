using System;
using System.IO.Abstractions;
using System.Linq;
using Pagene.Models;

namespace Pagene.Converter
{
    internal class Formatter
    {
        /*
        internal async IAsyncEnumerable<IFileInfo> ParseAllAsync()
        {
        }
        */
        internal BlogItem Parse(IFileInfo file)
        {
            using var reader = file.OpenText();
            string title = reader.ReadLine();
            if (!string.Equals(title.Substring(0, 2),"# "))
            {
                throw new FormatException();
            }
            var lines = file.FileSystem.File.ReadAllLines(file.FullName);
            var lastLine = lines.LastOrDefault();
            if (lastLine[0] != '[' || lastLine[^1] != ']')
            {
                throw new FormatException();
            }
            var tags = lastLine.Substring(1, lastLine.Length - 2).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag=>tag.Trim());
            return new BlogItem(
                title: title,
                content: string.Join('\n', lines),
                creationDate: file.CreationTimeUtc,
                modificationDate: file.LastWriteTimeUtc,
                tags: tags
                );
        }
    }
}
