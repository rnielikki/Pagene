using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagene.Reader.PostSerializer
{
    /// <summary>
    /// Parser for part of the format, line by line. Defines what the parser syntax sholud be, and how it works.
    /// </summary>
    public class FormatParser:IFormatParser
    {
        /// <summary>
        /// Parse Tag from certain format. The tag format is [tag1,tag2,tag3,...]
        /// </summary>
        /// <param name="raw">unparsed raw tag string.</param>
        /// <returns>String of tags as IEnumerable.</returns>
        public IEnumerable<string> ParseTag(string raw)
        {
            if (raw.Length < 2 || raw[0] != '[' || raw[^1] != ']')
            {
                throw new FormatException();
            }
            return raw[1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag=>tag.Trim()).Distinct();
        }
        /// <summary>
        /// Parse Title from certain format. The title format is same as markdown h1, "# title".
        /// </summary>
        /// <param name="raw">unparsed raw title string.</param>
        /// <returns>Title name, string.</returns>
        public string ParseTitle(string raw)
        {
            if (raw.Length < 2 || raw[0]!='#' || raw[1]!=' ')
            {
                throw new FormatException();
            }
            return raw[2..^0];
        }
    }
}
