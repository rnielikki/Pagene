using System;
using System.Collections.Generic;
using System.Text;

namespace Pagene.Models
{
    /// <summary>
    /// Tag metadata. used for meta.tags.json serializing.
    /// </summary>
    public class TagMeta
    {
        /// <summary>
        /// URL, where the tag file is.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Number of items that contains this tag.
        /// </summary>
        public int Count { get; set; }
    }
}
