namespace Pagene.Reader.PostSerializer
{
    /// <summary>
    /// Basic BlogPost serializer and deserializer interface for blog post reader and blog poster writer.
    /// for implemented version, see <see cref="PostSerializer" />.
    /// </summary>
    public interface IPostSerializer
    {
        /// <summary>
        /// Serialize <see cref="Models.BlogItem" /> to Stream.
        /// </summary>
        /// <param name="item">Target <see cref="Models.BlogItem" /> to serialize</param>
        /// <param name="stream">Target stream of string to read from.</param>
        System.Threading.Tasks.Task SerializeAsync(Models.BlogItem item, System.IO.Stream stream);
        /// <summary>
        /// Deseriazlie <see cref="Models.BlogItem" /> to string for writer.
        /// </summary>
        /// <param name="inputData">Stream of string for deserialization.</param>
        /// <returns>Deserialized <see cref="Models.BlogItem" />.</returns>
        System.Threading.Tasks.Task<Models.BlogItem> DeserializeAsync(System.IO.Stream inputData);
    }
}
