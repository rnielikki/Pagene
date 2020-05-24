using Pagene.Reader.PostSerializer;
using Pagene.Models;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Editor.Tests")]
namespace Pagene.Editor
{
    internal class BlogPostLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly FormatParser _formatParser = new FormatParser();
        private readonly PostSerializer _serializer = new PostSerializer();
        internal BlogPostLoader(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        internal BlogPostLoader() : this(new FileSystem()) { }

        internal List<FileTitlePair> LoadPosts()
        {
            List<FileTitlePair> posts = new List<FileTitlePair>();
            var files = _fileSystem.DirectoryInfo.FromDirectoryName("inputs/contents/").GetFiles("*.md", System.IO.SearchOption.TopDirectoryOnly)
                .OrderByDescending(file => file.CreationTimeUtc);
            foreach (var file in files)
            {
                posts.Add(LoadTitle(file));
            }
            return posts;
        }
        private FileTitlePair LoadTitle(IFileInfo file)
        {
            using var reader = file.OpenText();
            reader.ReadLine();
            string title = _formatParser.ParseTitle(reader.ReadLine());
            return new FileTitlePair(file.Name, title);
        }
        internal async System.Threading.Tasks.Task<BlogItem> GetBlogItem(string fileName)
        {
            using var fileStream = _fileSystem.File.Open(System.IO.Path.Combine("inputs/contents", fileName), System.IO.FileMode.Open);
            return await _serializer.DeserializeAsync(fileStream).ConfigureAwait(true);
        }
    }
}
