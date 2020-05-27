using System;
using System.Collections.Generic;
using System.Text;

namespace Pagene.Models
{
    /// <summary>
    /// Blog Entry Object for Tag List, for serialization
    /// </summary>
    public class BlogEntry
    {
        /// <summary>
        /// Title of the pgae.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Creation date of the page.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Relative path of the page.
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Tags that used by the post
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
