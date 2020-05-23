using Pagene.Models;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.Concurrent;
using System.IO.Abstractions.TestingHelpers;

namespace Pagene.Converter.Tests
{
    public class TagTest
    {
        [Fact]
        public void TagAddingTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { URL = "this-is-one.md" });
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese" }, new BlogEntry { URL = "this-is-two.md" });
            var tagManager = new TagManager(new MockFileSystem());
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);

            var resultDictionary = GetTagMap(tagManager);
            resultDictionary.Keys.OrderBy(str => str).Should().Equal(new string[] { "apple", "cheese", "ice cream", "juice", "orange" });
            var val = resultDictionary["cheese"];
            resultDictionary["cheese"].Keys.Should().Contain(entry1.URL)
                .And.Contain(entry2.URL);

            resultDictionary["orange"].Keys.Should().NotContain(entry1.URL)
                .And.Contain(entry2.URL);
        }
        [Fact]
        public async System.Threading.Tasks.Task TagGetTest()
        {
            var mockFileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { "input/contents/path.md", new MockFileData("-")}
                 }
            );
            var tagManager = new TagManager(mockFileSystem);
            var mockValue = new ConcurrentDictionary<string, BlogEntry>();
            mockValue.TryAdd("test.md", new BlogEntry { Title = "Custom Data", URL = "contents/test.md", });
            GetTagMap(tagManager).TryAdd("someTag", mockValue);
            await tagManager.Serialize();

            string serializedTag = mockFileSystem.File.ReadAllText("tags/meta.tags.json");

            tagManager = new TagManager(mockFileSystem);
            Assert.NotEmpty(GetTagMap(tagManager).Keys);
            Assert.Contains("someTag", GetTagMap(tagManager).Keys);
            await tagManager.Serialize();
            string reserializedTag = mockFileSystem.File.ReadAllText("tags/meta.tags.json");
            Assert.Equal(serializedTag, reserializedTag);
            Assert.True(mockFileSystem.FileExists("tags/sometag.json"));
            var tagContent = mockFileSystem.FileInfo.FromFileName("tags/sometag.json").Open(System.IO.FileMode.Open);
            var entries = Utf8Json.JsonSerializer.Deserialize<TagInfo>(tagContent);
            Assert.Equal("Custom Data", entries.Posts.Single().Title);
        }
        [Fact]
        public void TagRemovalTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { URL = "contents/uno.md" });
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese", "milk" }, new BlogEntry { URL = "contents/dos.md" });
            (var tags3, var entry3) = (new string[] { "apple", "juice", "milk", "cheese" }, new BlogEntry { URL = "contents/tres.md" });
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>()
            {
                { "tags/milk.json", new MockFileData("{\"milk\":[]}") }
            });
            var tagManager = new TagManager(fileSystem);
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);
            tagManager.AddTag(tags3, entry3);
            IEnumerable<string> removedTarget = new string[] { "dos.md", "tres.md" };
            tagManager.CleanTags(removedTarget);
            var resultDictionary = GetTagMap(tagManager);
            resultDictionary.Should().NotContainKey("milk");
            resultDictionary["cheese"].Should().ContainKey("uno.md")
                .And.NotContainKey("dos.md");
            fileSystem.FileExists("tags/milk.json").Should().BeFalse();
        }
        private ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> GetTagMap(TagManager instance) => typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(instance) as ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>;
    }

}
