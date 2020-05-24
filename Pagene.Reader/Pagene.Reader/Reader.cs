using Pagene.Models;
using Pagene.Reader.PostSerializer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pagene.Reader
{
    /// <summary>
    /// Reader for converted blog posts.
    /// </summary>
    public class Reader
    {
        private readonly IPostSerializer _serializer;
        /// <summary>
        /// Create reader for converted static blog posts.
        /// </summary>
        /// <param name="serializer">Post serializer that reads other than date info.</param>
        public Reader(IPostSerializer serializer)
        {
            _serializer = serializer;
        }
        /// <summary>
        /// Create reader for converted blog posts.
        /// </summary>
        public Reader() => _serializer = new PostSerializer.PostSerializer();

        /// <summary>
        /// Change converted static blog post stream to BlogItem data.
        /// </summary>
        /// <param name="stream">Stream of string for read post.</param>
        /// <returns><see cref="BlogItem" /> from converted static file content.</returns>
        public async Task<BlogItem> ReadPostAsync(Stream stream)
        {
            using StreamReader reader = new StreamReader(stream);
            DateTime creationDate = await ParseDate(reader).ConfigureAwait(false);
            DateTime modificationDate = await ParseDate(reader).ConfigureAwait(false);
            BlogItem item = await _serializer.DeserializeAsync(stream).ConfigureAwait(false);
            item.CreationDate = creationDate;
            item.ModificationDate = modificationDate;
            return item;
        }

        private static async Task<DateTime> ParseDate(StreamReader reader)
        {
            char[] buffer = new char[2];
            await reader.ReadAsync(buffer).ConfigureAwait(false);
            if (buffer[0] != '>' || buffer[1] != ' ')
            {
                new FormatException();
            }
            string date = await reader.ReadLineAsync().ConfigureAwait(false);
            return DateTime.Parse(date); //let it throw if the format is wrong.
        }
    }
}
