using Pagene.Converter.Tests.Models;
using System.IO.Abstractions;
using Xunit;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;

namespace Pagene.Converter.Tests
{
    public class FormatterTest
    {
        private readonly IFormatter _formatter = new Formatter("contents");
        [Theory]
        [MemberData(nameof(GetValidationSet))]
        public async System.Threading.Tasks.Task GetBlogHeadTest(IFileInfo sample, IEnumerable<string> tags, string title, bool valid)
        {
            using var fileStream = sample.Open(System.IO.FileMode.Open);
            if (!valid)
            {
                await Assert.ThrowsAsync<System.FormatException>(() => _formatter.GetBlogHead(sample, fileStream));
            }
            else
            {
                (var entryTags, var entry) = await _formatter.GetBlogHead(sample, fileStream);
                entry.Title.Should().Be(title);
                entry.URL.Should().Be("contents/"+sample.Name);
                entryTags.Should().BeEquivalentTo(tags);
            }
            //formatter.GetBlogHead();
        }

        //will move to reader part
        /*
        [Theory]
        [MemberData(nameof(GetValidationSet))]
        public async System.Threading.Tasks.Task TitleValidationTest(IFileInfo sample, string title, bool valid)
        {
            using var fileStream = sample.Open(System.IO.FileMode.Open);
            if (!valid)
            {
                await Assert.ThrowsAsync<FormatException>(() => formatter.ParseAsync(sample, fileStream));
            }
            else
            {
                BlogItem result = await formatter.ParseAsync(sample, fileStream);
                Assert.Equal("# " + title, result.Title);
            }
        }

        [Fact]
        public async System.Threading.Tasks.Task TagParseTest()
        {
            using var stream1 = FormatterTestModel.file1.Open(System.IO.FileMode.Open);
            using var stream2 = FormatterTestModel.file2.Open(System.IO.FileMode.Open);
            BlogItem item1 = await formatter.ParseAsync(FormatterTestModel.file1, stream1);
            BlogItem item2 = await formatter.ParseAsync(FormatterTestModel.file2, stream2);
            Assert.Equal(TagsTestModel.tags1, item1.Tags.OrderBy(str => str));
            Assert.Equal(TagsTestModel.tags2, item2.Tags.OrderBy(str => str));
        }

        public static TheoryData<DateTime, DateTime> DateSamples() => new TheoryData<DateTime, DateTime>() {
       { new DateTime(2015, 10, 11), new DateTime(2015, 11, 11) },
       { new DateTime(2016, 3, 12), new DateTime(2016, 3, 12) }
   };
        */
        public static TheoryData<IFileInfo, IEnumerable<string>, string, bool> GetValidationSet() => new TheoryData<IFileInfo, IEnumerable<string>, string, bool>() {
            { FormatterTestModel.file1, TagsTestModel.tags1, FormatterTestModel.title1, true },
            { FormatterTestModel.file2, TagsTestModel.tags2, FormatterTestModel.title2, true },
            { FormatterTestModel.error1, null, string.Empty, false },
            { FormatterTestModel.error2, null, string.Empty, false }
        };
    }
}
