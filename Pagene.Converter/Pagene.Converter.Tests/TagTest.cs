using Pagene.Models;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.Concurrent;

namespace Pagene.Converter.Tests
{
    public class TagTest
    {
        [Fact]
        public void TagAddingTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { URL="this-is-one.md"});
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese" }, new BlogEntry { URL="this-is-two.md" });
            using var tagManager = new TagManager(new System.IO.Abstractions.TestingHelpers.MockFileSystem());
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);

            var resultDictionary = typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(tagManager) as ConcurrentDictionary<string, ConcurrentDictionary<string, BlogEntry>>;

            resultDictionary.Keys.OrderBy(str=>str).Should().Equal(new string[] { "apple", "cheese", "ice cream", "juice", "orange" });
            var val = resultDictionary["cheese"];
            resultDictionary["cheese"].Keys.Should().Contain(entry1.URL)
                .And.Contain(entry2.URL);

            resultDictionary["orange"].Keys.Should().NotContain(entry1.URL)
                .And.Contain(entry2.URL);
        }
    }
}
