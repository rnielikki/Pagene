using Pagene.Models;
using System.Collections.Generic;
using Xunit;
using Shouldly;

namespace Pagene.Converter.Tests
{
    public class TagTest
    {
        [Fact]
        public void TagAddingTest()
        {
            var data1 = (new string[] { "cheese", "apple", "ice cream" }, new BlogEntry { Title = "data1" });
            var data2 = (new string[] { "orange", "juice", "apple", "cheese" }, new BlogEntry { Title = "data2" });
            TagManager.AddTag(data1.Item1, data1.Item2);
            TagManager.AddTag(data2.Item1, data2.Item2);

            var resultDictionary = typeof(TagManager)
                .GetField("_tagMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .GetValue(null) as Dictionary<string, BlogEntry[]>;

            resultDictionary.Keys.ShouldBe(new string[] { "apple", "cheese", "ice cream", "juice", "orange" });
            resultDictionary["cheese"].ShouldContain(data => data.Title == "data1");
            resultDictionary["cheese"].ShouldContain(data => data.Title == "data2");
            resultDictionary["orange"].ShouldNotContain(data => data.Title == "data1");
            resultDictionary["orange"].ShouldContain(data => data.Title == "data2");
        }
    }
}
