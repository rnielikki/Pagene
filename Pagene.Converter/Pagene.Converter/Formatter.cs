using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Pagene.Models;
using Pagene.Reader.PostSerializer;

namespace Pagene.Converter
{
    internal class Formatter : IFormatter
    {
        private readonly string _path;
        private readonly FormatParser _parser = new FormatParser();
        internal Formatter(string path)
        {
            _path = path;
        }
        async System.Threading.Tasks.Task<(IEnumerable<string>, BlogEntry)> IFormatter.GetBlogHead(IFileInfo info, Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            var tags = _parser.ParseTag(await reader.ReadLineAsync());
            var title = _parser.ParseTitle(await reader.ReadLineAsync());
            return (tags, new BlogEntry
            {
                Title = title,
                Date = info.CreationTimeUtc,
                URL = $"{_path}/{info.Name}"
            });
        }
    }
}
