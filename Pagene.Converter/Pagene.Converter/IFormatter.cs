using Pagene.Models;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace Pagene.Converter
{
    interface IFormatter
    {
        internal Task<BlogEntry> GetBlogHeadAsync(IFileInfo info, StreamReader reader);
        bool UseSummary { get; set; }
        int SummaryLength { get; set; }
        void EnableSummary() => UseSummary = true;
        void DisableSummary() => UseSummary = false;
        string GetSummary(string original);
        Task<BlogEntry> GetBlogEntryAsync(IFileInfo info);
    }
}
