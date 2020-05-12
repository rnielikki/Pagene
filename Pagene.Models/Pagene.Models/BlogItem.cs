using System;
using System.Collections.Generic;
using System.Linq;

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
        public string Title { get; private set; }
        /// <summary>
        /// Content of the blog item. It is part of static blog post content.
        /// </summary>
        /// <note>Content does not contain tags, but still contains title.</note>
        public string Content { get; private set; }
        /// <summary>
        /// The time, when the post was created.
        /// </summary>
        /// <remarks><see cref="CreationDate" /> and <see cref="ModificationDate" /> are from file metadata and may contain invalid data if original file's metadata is invalid.
        /// The program doesn't check if the metadata is valid.</remarks>
        public DateTime CreationDate { get; private set; }
        /// <summary>
        /// The time, when the post was modified.
        /// </summary>
        /// <remarks><see cref="CreationDate" /> and <see cref="ModificationDate" /> are from file metadata and may contain invalid data if original file's metadata is invalid. 
        /// The program doesn't check if the metadata is valid.</remarks>
        public DateTime ModificationDate { get; private set; }
        /// <summary>
        /// Tags of the post.
        /// </summary>
        /// <note>If you put the duplicated tags on the constructor, it automatically removes duplications.</note>
        public IEnumerable<string> Tags { get; private set; }
        /// <summary>
        /// Create new BlogItem.
        /// </summary>
        /// <param name="title">Title for the blog post.</param>
        /// <param name="content">Content for the blog post.</param>
        /// <param name="creationDate">Creation date from file metadata.</param>
        /// <param name="modificationDate">Modification date from file metadata.</param>
        /// <param name="tags">Tags of the blog post.</param>
        public BlogItem(string title, string content, DateTime creationDate, DateTime modificationDate, IEnumerable<string> tags){
            Title = title;
            Content = content;
            CreationDate = creationDate;
            ModificationDate = modificationDate;
            Tags = tags.Distinct();
        }
    }
}
