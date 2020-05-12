using Pagene.Converter.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Pagene.Models;

namespace Pagene.Converter.Tests
{
    public class StaticWriterTest
    {
        private readonly staticWriter = new StaticWriter();
        [Fact]
        public void TagFormatterTest()
        {
            var anyTime = new DateTime();
            BlogItem item1 = new BlogItem(
                title:"",
                content: "",
                tags:TagsTestModel.tags1,
                creationDate:anyTime,
                modificationDate:anyTime
            );
            BlogItem item2 = new BlogItem(
                title:"",
                content: "",
                tags:TagsTestModel.tags2,
                creationDate:anyTime,
                modificationDate:anyTime
            );
            Dictionary<string, BlogEntry[]> tagObject = staticWriter.GetTags();
            Assert.Equal(tagObject.Keys.AsEnumerable(), TagsTestModel.intersection);
        }
    }
}
