using System.Collections.Generic;

namespace Pagene.Reader.PostSerializer
{
    /// <summary>
    /// Interface for format parser.
    /// </summary>
    public interface IFormatParser
    {
        /// <summary>
        /// Parse Tag from certain format.
        /// </summary>
        /// <param name="raw">unparsed raw tag string.</param>
        /// <returns>String of tags as IEnumerable.</returns>
        IEnumerable<string> ParseTag(string raw);
        /// <summary>
        /// Parse Title from certain format.
        /// </summary>
        /// <param name="raw">unparsed raw title string.</param>
        /// <returns>Title name, string.</returns>
        string ParseTitle(string raw);
    }
}
