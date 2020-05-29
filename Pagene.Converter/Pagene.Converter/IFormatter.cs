using Pagene.Models;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace Pagene.Converter
{
    interface IFormatter
    {
        internal Task<BlogEntry> GetBlogHead(IFileInfo info, System.IO.Stream stream);
        internal Task<BlogEntry> GetBlogHead(IFileInfo info, System.IO.Stream stream, int length);
        internal Task<BlogEntry> GetBlogHead(IFileInfo info);
        bool UseSummary { get; set; }
        void EnableSummary() => UseSummary = true;
        void DisableSummary() => UseSummary = false;
    }
}
