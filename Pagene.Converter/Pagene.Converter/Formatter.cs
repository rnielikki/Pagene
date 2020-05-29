using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Pagene.BlogSettings;
using Pagene.Models;
using Pagene.Reader.PostSerializer;

namespace Pagene.Converter
{
    internal class Formatter : IFormatter
    {
        private readonly string _path;
        private readonly IFormatParser _parser;
        public bool UseSummary { get; set; }

        internal Formatter(string path, IFormatParser parser)
        {
            UseSummary = ConvertingInfo.UseSummary;
            _path = path;
            _parser = parser;
        }
        internal Formatter(string path):this(path,new FormatParser()) { }
        async Task<BlogEntry> IFormatter.GetBlogHead(IFileInfo info)
        {
            using Stream stream = info.OpenRead();
            return await (this as IFormatter).GetBlogHead(info, stream, ConvertingInfo.SummaryLength).ConfigureAwait(false);
        }

        async Task<BlogEntry> IFormatter.GetBlogHead(IFileInfo info, Stream stream) => await (this as IFormatter).GetBlogHead(info, stream, ConvertingInfo.SummaryLength).ConfigureAwait(false);
        async Task<BlogEntry> IFormatter.GetBlogHead(IFileInfo info, Stream stream, int length)
        {
            StreamReader reader = new StreamReader(stream);
            try
            {
                var tags = _parser.ParseTag(await reader.ReadLineAsync().ConfigureAwait(false));
                var title = _parser.ParseTitle(await reader.ReadLineAsync().ConfigureAwait(false));
                var summary = "";
                if (UseSummary)
                {
                    int position = checked((int)stream.Position);
                    char[] buffer = new char[length];
                    await reader.ReadAsync(buffer, 0, length).ConfigureAwait(false);
                    stream.Position = position;
                    summary = new string(buffer).Trim('\0').Replace('\r', ' ').Replace('\n', ' ');
                }
                return new BlogEntry
                {
                    Title = title,
                    Date = info.CreationTimeUtc,
                    URL = _path + info.Name,
                    Summary = summary,
                    Tags = tags
                };
            }
            catch (FormatException)
            {
                throw new FormatException(info.Name);
            }
        }
    }
}
