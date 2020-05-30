using Pagene.Models;
using System.IO;

namespace Pagene.Reader.PostSerializer
{
    /// <summary>
    /// Basic BlogPost serializer and deserializer for blog post reader and blog poster writer.
    /// </summary>
    public class PostSerializer : IPostSerializer
    {
        private readonly FormatParser _formatParser = new FormatParser();
        /// <summary>
        /// Serialize <see cref="BlogItem" /> to Stream.
        /// </summary>
        /// <param name="item">Target <see cref="BlogItem" /> to serialize</param>
        /// <param name="stream">Target stream of string to read from.</param>
        /// <remarks>This *doesn't* rewind or truncate stream automatically.
        /// If it's needed, rewind or truncate the stream manually before using it.</remarks>
        public async System.Threading.Tasks.Task SerializeAsync(BlogItem item, Stream stream)
        {
            //not "using writer" -> does not dispose after this method
            StreamWriter writer = new StreamWriter(stream);
            await writer.WriteAsync("[").ConfigureAwait(false);
            await writer.WriteAsync(string.Join(',', item.Tags)).ConfigureAwait(false);
            await writer.WriteLineAsync("]").ConfigureAwait(false);
            await writer.WriteAsync("# ").ConfigureAwait(false);
            await writer.WriteLineAsync(item.Title).ConfigureAwait(false);
            await writer.WriteAsync(item.Content).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Deseriazlie <see cref="BlogItem" /> to string for writer.
        /// </summary>
        /// <param name="inputData">Stream of string for deserialization.</param>
        /// <returns>Deserialized <see cref="BlogItem" />.</returns>
        public async System.Threading.Tasks.Task<BlogItem> DeserializeAsync(Stream inputData)
        {
            //using reader -> dispose after this method
            using StreamReader reader = new StreamReader(inputData);
            var tags = _formatParser.ParseTag(await reader.ReadLineAsync().ConfigureAwait(false));
            var title = _formatParser.ParseTitle(await reader.ReadLineAsync().ConfigureAwait(false));
            return new BlogItem
            {
                Title = title,
                Content = await reader.ReadToEndAsync().ConfigureAwait(false),
                Tags = tags
            };
        }
    }
}
