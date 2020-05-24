using System;
using System.Collections.Generic;

namespace Pagene.Models
{
    /// <summary>
    /// Blog Item Object from raw MarkDown format.
    /// </summary>
    public class BlogItem
    {
        /// <summary>
        /// Title of the item. This is used for creating blog entry data.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Content of the blog item. It is part of static blog post content.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// The time, when the post was created.
        /// </summary>
        /// <remarks><see cref="CreationDate" /> and <see cref="ModificationDate" /> are from file metadata and may contain invalid data if original file's metadata is invalid.
        /// The program doesn't check if the metadata is valid.</remarks>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// The time, when the post was modified.
        /// </summary>
        /// <remarks><see cref="CreationDate" /> and <see cref="ModificationDate" /> are from file metadata and may contain invalid data if original file's metadata is invalid.
        /// The program doesn't check if the metadata is valid.</remarks>
        public DateTime ModificationDate { get; set; }
        /// <summary>
        /// Tags of the post.
        /// </summary>
        /// <note>If you put the duplicated tags on the constructor, it automatically removes duplications.</note>
        public IEnumerable<string> Tags { get; set; }
    }
}
