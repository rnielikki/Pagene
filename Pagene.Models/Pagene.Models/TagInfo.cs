using System.Collections.Generic;

namespace Pagene.Models
{
    /// <summary>
    /// Blog posts from one tag.
    /// </summary>
    public class TagInfo
    {
        public string Tag { get; }
        public IEnumerable<BlogEntry> Posts { get; }
        public TagInfo(string tag, IEnumerable<BlogEntry> posts)
        {
            Tag = tag;
            Posts = posts;
        }
    }
}
