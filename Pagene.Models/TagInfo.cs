using System.Collections.Generic;

namespace Pagene.Models
{
    /// <summary>
    /// Blog posts from one tag.
    /// </summary>
    public class TagInfo
    {
        public string Tag { get; private set; }
        public IEnumerable<BlogEntry> Posts { get; private set; }
        public TagInfo(string tag, IEnumerable<BlogEntry> posts)
        {
            Tag = tag;
            Posts = posts;
        }
    }
}
