using Pagene.Reader.PostSerializer;
using Pagene.Models;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Utf8Json;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Editor.Tests")]
namespace Pagene.Editor
{
    internal class BlogPostLoader
    {
        private readonly IFileSystem _fileSystem;
        private readonly FormatParser _formatParser = new FormatParser();
        private readonly IPostSerializer _serializer;
        private readonly NamingLogic _namingLogic;
        internal BlogPostLoader(IFileSystem fileSystem, IPostSerializer mockSerializer)
        {
            _fileSystem = fileSystem;
            _serializer = mockSerializer;
            _namingLogic = new NamingLogic(fileSystem);
        }
        internal BlogPostLoader() : this(new FileSystem(), new PostSerializer()) { }

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
            using var fileStream = GetFileStream(fileName, System.IO.FileMode.Open);
            return await _serializer.DeserializeAsync(fileStream).ConfigureAwait(true);
        }
        internal async System.Threading.Tasks.Task SaveBlogItem(BlogItem item, string fileName)
        {
            using var fileStream = GetFileStream(fileName, System.IO.FileMode.Create);
            await _serializer.SerializeAsync(item, fileStream).ConfigureAwait(true);
        }
        private System.IO.Stream GetFileStream(string fileName, System.IO.FileMode mode) => _fileSystem.File.Open(System.IO.Path.Combine("inputs/contents", fileName), mode);
        internal void RemovePost(string fileName) => _fileSystem.File.Delete($"inputs/contents/{fileName}");

        //wait until the methods get right place:
        internal IEnumerable<string> GetTags()
        {
            const string metaTagPath = "tags/meta.tags.json";
            if (!_fileSystem.File.Exists(metaTagPath)) return Enumerable.Empty<string>();
            using var stream = _fileSystem.File.Open(metaTagPath, System.IO.FileMode.Open);
            return JsonSerializer.Deserialize<Dictionary<string, int>>(stream).Keys;
        }
        internal string GetNameFromTitle(string title) => _namingLogic.GetName(title);
    }
}
