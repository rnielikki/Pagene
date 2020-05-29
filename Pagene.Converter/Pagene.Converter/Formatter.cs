using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
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
        async Task<BlogEntry> IFormatter.GetBlogHead(IFileInfo info)
        {
            using Stream stream = info.OpenRead();
            return await (this as IFormatter).GetBlogHead(info, stream).ConfigureAwait(false);
        }

        async Task<BlogEntry> IFormatter.GetBlogHead(IFileInfo info, Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            try
            {
                var tags = _parser.ParseTag(await reader.ReadLineAsync().ConfigureAwait(false));
                var title = _parser.ParseTitle(await reader.ReadLineAsync().ConfigureAwait(false));
                var summary = "";
                /*
                if (ConvertingInfo.UseSummary)
                {
                }
                */
                return new BlogEntry
                {
                    Title = title,
                    Date = info.CreationTimeUtc,
                    URL = _path + info.Name,
                    //Summary = summary,
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
