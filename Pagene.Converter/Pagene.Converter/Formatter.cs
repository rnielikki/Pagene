﻿using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Pagene.BlogSettings;
using Pagene.Models;
using Pagene.Reader.PostSerializer;

namespace Pagene.Converter
{
    /// <inheritdoc cref="IFormatter""/>
    internal class Formatter : IFormatter
    {
        private readonly string _path;
        private readonly IFormatParser _parser;
        public bool UseSummary { get; set; }
        public int SummaryLength { get; set; } = ConvertingInfo.SummaryLength;

        internal Formatter(string path, IFormatParser parser)
        {
            UseSummary = ConvertingInfo.UseSummary;
            _path = path;
            _parser = parser;
        }
        internal Formatter():this(RoutePathInfo.ContentPath, new FormatParser()) { }

        async Task<BlogEntry> IFormatter.GetBlogHeadAsync(IFileInfo info, StreamReader reader)
        {
            try
            {
                var tags = _parser.ParseTag(await reader.ReadLineAsync().ConfigureAwait(false));
                var title = _parser.ParseTitle(await reader.ReadLineAsync().ConfigureAwait(false));

                return new BlogEntry
                {
                    Title = title,
                    Date = info.CreationTime,
                    Url = Path.Combine(_path, Path.GetFileNameWithoutExtension(info.Name)).Replace('\\', '/'),
                    Summary = "",
                    Tags = tags
                };
            }
            catch (FormatException)
            {
                throw new FormatException(info.Name);
            }
        }

        /// <summary>
        /// Use only if you don't reuse the stream/stream reader anymore.
        /// </summary>
        async Task<BlogEntry> IFormatter.GetBlogEntryAsync(IFileInfo info)
        {
            using StreamReader reader = new(info.OpenRead());
            var entry = await (this as IFormatter).GetBlogHeadAsync(info, reader).ConfigureAwait(false);
            entry.Summary = await GetSummaryAsync(reader).ConfigureAwait(false);
            return entry;
        }
        private async Task<string> GetSummaryAsync(StreamReader reader)
        {
            if (UseSummary)
            {
                char[] buffer = new char[SummaryLength];
                await reader.ReadAsync(buffer, 0, SummaryLength).ConfigureAwait(false);
                return new string(buffer).Trim('\0').Replace('\r', ' ').Replace('\n', ' ');
            }
            else
            {
                return "";
            }
        }
        string IFormatter.GetSummary(string original) => (original.Length <= SummaryLength)?original:original.Substring(0, SummaryLength);
    }
}
