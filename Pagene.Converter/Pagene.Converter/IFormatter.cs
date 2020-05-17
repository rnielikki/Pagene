
namespace Pagene.Converter
{
    internal interface IFormatter
    {
        internal System.Threading.Tasks.Task<(System.Collections.Generic.IEnumerable<string>, Models.BlogEntry)> GetBlogHead(System.IO.Abstractions.IFileInfo info, System.IO.Stream stream);
    }
}
