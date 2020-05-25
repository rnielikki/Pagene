using Pagene.Models;
using Pagene.Reader.PostSerializer;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using Moq;

namespace Pagene.Editor.Tests
{
    public class SaveTest
    {
        [Theory]
        [MemberData(nameof(SaveItems))]
        public async System.Threading.Tasks.Task BlogPostSaveTest(KeyValuePair<string, MockFileData> data, string fileName)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>() { { data.Key, data.Value} });
            var loader = new BlogPostLoader(fileSystem, new PostSerializer());

            var item = new BlogItem { Title = "title", Content = "x", Tags = new string[] { "a", "b", "c" } };
            await loader.SaveBlogItem(item, fileName).ConfigureAwait(true);
            Assert.True(fileSystem.FileExists($"inputs/contents/{fileName}"));
            string[] fileContents = fileSystem.File.ReadAllLines($"inputs/contents/{fileName}");
            Assert.Equal("[a,b,c]", fileContents[0]);
            Assert.Equal("# title", fileContents[1]);
            Assert.Equal("x", fileContents[2]);
        }
        public static TheoryData<KeyValuePair<string, MockFileData>, string> SaveItems() =>
            new TheoryData<KeyValuePair<string, MockFileData>, string>()
            {
                {KeyValuePair.Create( "inputs/contents/nonexists.md", new MockFileData("hello")), "newFile.md"}, //createTest
                {KeyValuePair.Create( "inputs/contents/exists.md", new MockFileData("asdf")), "exists.md"} //editTest
            };
    }
}
