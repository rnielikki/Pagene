﻿using Pagene.Models;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.Concurrent;
using System.IO.Abstractions.TestingHelpers;
using Pagene.BlogSettings;
using System.IO;

namespace Pagene.Converter.Tests
{
    public class TagTest
    {
        [Fact]
        public void TagAddingTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { Url = "this-is-one" });
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese" }, new BlogEntry { Url = "this-is-two" });
            var tagManager = new TagManager(new MockFileSystem());
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);

            var resultDictionary = GetTagMap(tagManager);
            resultDictionary.Keys.OrderBy(str => str).Should().Equal(new string[] { "apple", "cheese", "ice cream", "juice", "orange" });
            resultDictionary["cheese"].Keys.Should().Contain(entry1.Url)
                .And.Contain(entry2.Url);

            resultDictionary["orange"].Keys.Should().NotContain(entry1.Url)
                .And.Contain(entry2.Url);
        }
        [Fact]
        public async System.Threading.Tasks.Task TagGetTest()
        {
            var mockFileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { Models.FormatterTestModel.InputContentPath+"path.md", new MockFileData("-")}
                 }
            );
            var tagManager = new TagManager(mockFileSystem);
            var mockValue = new ConcurrentDictionary<string, BlogEntry>();
            mockValue.TryAdd("test.md", new BlogEntry { Title = "Custom Data", Url = Models.FormatterTestModel.OutputContentPath+"test.md", });
            GetTagMap(tagManager).TryAdd("someTag", mockValue);
            await tagManager.Serialize().ConfigureAwait(false);

            string serializedTag = mockFileSystem.File.ReadAllText(AppPathInfo.BlogTagPath+"meta.tags.json");

            tagManager = new TagManager(mockFileSystem);
            Assert.NotEmpty(GetTagMap(tagManager).Keys);
            Assert.Contains("someTag", GetTagMap(tagManager).Keys);
            await tagManager.Serialize().ConfigureAwait(false);
            string reserializedTag = mockFileSystem.File.ReadAllText(AppPathInfo.BlogTagPath+"meta.tags.json");
            Assert.Equal(serializedTag, reserializedTag);

            Assert.True(mockFileSystem.FileExists(AppPathInfo.BlogTagPath+"0.json"));
            var tagContent = mockFileSystem.FileInfo.FromFileName(AppPathInfo.BlogTagPath+"0.json").Open(FileMode.Open);
            var entries = Utf8Json.JsonSerializer.Deserialize<TagInfo>(tagContent);
            Assert.Equal("Custom Data", entries.Posts.Single().Title);
        }
        [Fact]
        public void TagRemovalTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { Url = Path.Combine(RoutePathInfo.ContentPath, "uno").Replace('\\', '/')});
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese", "milk" }, new BlogEntry { Url = Path.Combine(RoutePathInfo.ContentPath, "dos").Replace('\\', '/')});
            (var tags3, var entry3) = (new string[] { "apple", "juice", "milk", "cheese" }, new BlogEntry { Url = Path.Combine(RoutePathInfo.ContentPath, "tres").Replace('\\', '/')});
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>()
            {
                { AppPathInfo.BlogTagPath+"milk.json", new MockFileData("{\"milk\":[]}") }
            });
            var tagManager = new TagManager(fileSystem);
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);
            tagManager.AddTag(tags3, entry3);
            IEnumerable<string> removedTarget = new string[] { "dos.md", "tres.md" };
            tagManager.Clean(removedTarget);

            var resultDictionary = GetTagMap(tagManager);
            resultDictionary.Should().NotContainKey("milk");
            resultDictionary["cheese"].Should().ContainKey(Path.Combine(RoutePathInfo.ContentPath, "uno").Replace('\\', '/'))
                .And.NotContainKey(Path.Combine(RoutePathInfo.ContentPath, "dos").Replace('\\', '/'));
            tagManager.GetRemovedTags().Should().Contain("milk");
        }
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>> GetTagMap(TagManager instance) => typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(instance) as ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>;
    }
}
