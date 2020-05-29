using System.Collections.Generic;

namespace Pagene.Models
{
    /// <summary>
    /// Blog posts from one tag, for serialization.
    /// </summary>
    public class TagInfo
    {
        /// <summary>
        /// Name of the tag. This is declared because Windows files are case-insensitive.
        /// </summary>
        public string Tag { get; }
        /// <summary>
        /// Posts that contain this tag.
        /// </summary>
        public IEnumerable<BlogEntry> Posts { get; }
    }
}
