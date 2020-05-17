using Pagene.Models;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Pagene.Converter.Tests
{
    public class TagTest
    {
        [Fact]
        public void TagAddingTest()
        {
            (var tags1, var entry1) = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry());
            (var tags2, var entry2) = (new string[] { "orange", "juice", "apple", "cheese" }, new BlogEntry());
            using var tagManager = new TagManager(new System.IO.Abstractions.TestingHelpers.MockFileSystem());
            tagManager.AddTag(tags1, entry1);
            tagManager.AddTag(tags2, entry2);

            var resultDictionary = typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(tagManager) as Dictionary<string, List<BlogEntry>>;

            resultDictionary.Keys.OrderBy(str=>str).Should().Equal(new string[] { "apple", "cheese", "ice cream", "juice", "orange" });
            resultDictionary["cheese"].Should().Contain(entry1)
                .And.Contain(entry2);

            resultDictionary["orange"].Should().NotContain(entry1)
                .And.Contain(entry2);
        }
    }
}
