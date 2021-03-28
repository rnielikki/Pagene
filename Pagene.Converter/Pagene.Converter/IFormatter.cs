using Pagene.Models;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace Pagene.Converter
{
    /// <summary>
    /// Defines post file text formats. It also parses and gets blog parts by the format.
    /// </summary>
    internal interface IFormatter
    {
        /// <summary>
        /// Gets blog title and basic post informations *without summary*.
        /// </summary>
        /// <param name="info">The blog fileinfo to get the blog head.</param>
        /// <param name="reader"></param>
        /// <remarks>Remember to add summary using GetSummary() if you need. You can use <see cref="GetBlogEntryAsync(IFileInfo)"/> to get entry with summary.</remarks>
        /// <remarks>The stream reader should be identitical to file info. The parameters are duplicated because of stream reusing.</remarks>
        internal Task<BlogEntry> GetBlogHeadAsync(IFileInfo info, StreamReader reader);
        /// <summary>
        /// Defines if the formatter uses summary. This is defined in <see cref="ConvertingInfo"/>.
        /// </summary>
        bool UseSummary { get; set; }
        /// <summary>
        /// Defines maximum length of the summary.
        /// </summary>
        int SummaryLength { get; set; }
        void EnableSummary() => UseSummary = true;
        void DisableSummary() => UseSummary = false;

        /// <summary>
        /// If string is over <see cref="SummaryLength"/>, it cuts the string.
        /// </summary>
        /// <param name="original">Original string to cut.</param>
        string GetSummary(string original);

        /// <summary>
        /// Gets blog entry, with summary.
        /// </summary>
        /// <param name="info">The blog fileinfo to get the blog entry.</param>
        Task<BlogEntry> GetBlogEntryAsync(IFileInfo info);
    }
}
