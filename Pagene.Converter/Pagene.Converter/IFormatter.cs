
namespace Pagene.Converter
{
    internal interface IFormatter
    {
        System.Threading.Tasks.Task<Models.BlogEntry> GetBlogHead(System.IO.Abstractions.IFileInfo info);
        System.Threading.Tasks.Task<Models.BlogEntry> GetBlogHead(System.IO.Abstractions.IFileInfo info, System.IO.Stream stream);
    }
}
