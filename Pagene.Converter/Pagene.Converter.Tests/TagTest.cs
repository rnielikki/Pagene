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
            using var tagManager = new TagManager(new MockFileSystem());
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
            using (var tagManager = new TagManager(mockFileSystem))
            {
                await tagManager.Serialize();
            }
            string serializedTag = mockFileSystem.File.ReadAllText("meta.tags.json");
            using (var tagManager = new TagManager(mockFileSystem))
            {
                await tagManager.Serialize();
            }
            string reserializedTag = mockFileSystem.File.ReadAllText("meta.tags.json");
            Assert.Equal(serializedTag, reserializedTag);
        }
        [Fact]
        public void TagRemovalTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { URL = "contents/uno.md" });
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese", "milk" }, new BlogEntry { URL = "contents/dos.md" });
            (var tags3, var entry3) = (new string[] { "apple", "juice", "milk", "cheese" }, new BlogEntry { URL = "contents/tres.md" });
            using var tagManager = new TagManager(new MockFileSystem());
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);
            tagManager.AddTag(tags3, entry3);
            IEnumerable<string> removedTarget = new string[] { "dos.md", "tres.md" };
            tagManager.CleanTags(removedTarget);
            var resultDictionary = GetTagMap(tagManager);
            resultDictionary.Should().NotContainKey("milk");
            resultDictionary["cheese"].Should().ContainKey("contents/uno.md")
                .And.NotContainKey("contents/dos.md");
        }
        private ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> GetTagMap(TagManager instance)=>typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(instance) as ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>;
    }

}
