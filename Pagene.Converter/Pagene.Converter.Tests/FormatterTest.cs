using Pagene.Converter.Tests.Models;
using System.IO.Abstractions;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Pagene.BlogSettings;
using System.IO;
using Moq;
using Pagene.Reader.PostSerializer;
using System.Text;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;
using System;

namespace Pagene.Converter.Tests
{
    public class FormatterTest
    {
        [Theory]
        [MemberData(nameof(GetValidationSet))]
        public async System.Threading.Tasks.Task GetBlogHeadTest(IFileInfo sample, IEnumerable<string> tags, string title, bool valid)
        {
            IFormatter formatter = new Formatter(AppPathInfo.ContentPath);
            formatter.DisableSummary();
            using var fileStream = sample.Open(FileMode.Open);
            if (!valid)
            {
                await Assert.ThrowsAsync<FormatException>(() => formatter.GetBlogHead(sample, fileStream)).ConfigureAwait(false);
            }
            else
            {
                var entry = await formatter.GetBlogHead(sample, fileStream).ConfigureAwait(false);
                var entryTags = entry.Tags;
                entry.Title.Should().Be(title);
                entry.Url.Should().Be(AppPathInfo.ContentPath+sample.Name);
                entryTags.Should().BeEquivalentTo(tags);
            }
        }
        [Theory]
        [InlineData("short", "short")]
        [InlineData("abcdefghijklmnop", "abcdefg")]
        public async System.Threading.Tasks.Task SummaryTest(string rawData, string expected)
        {
            var data = Environment.NewLine + Environment.NewLine + rawData;
            var parserMock = new Mock<IFormatParser>();
            parserMock.Setup(parser => parser.ParseTag(It.IsAny<string>())).Returns(Enumerable.Empty<string>);
            parserMock.Setup(parser => parser.ParseTitle(It.IsAny<string>())).Returns(string.Empty);
            IFormatter formatter = new Formatter(AppPathInfo.ContentPath, parserMock.Object);
            formatter.EnableSummary();

            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            var mockFileSystem = new MockFileSystem();
            const string anyFile = "asdf.md";
            var info = new MockFileInfo(mockFileSystem, anyFile);
            mockFileSystem.AddFile(anyFile, new MockFileData(""));
            var summary = (await formatter.GetBlogHead(info, stream, 7).ConfigureAwait(false)).Summary;

            Assert.Equal(expected, summary);
        }

         public static TheoryData<IFileInfo, IEnumerable<string>, string, bool> GetValidationSet() => new TheoryData<IFileInfo, IEnumerable<string>, string, bool>() {
            { FormatterTestModel.file1, TagsTestModel.tags1, FormatterTestModel.title1, true },
            { FormatterTestModel.file2, TagsTestModel.tags2, FormatterTestModel.title2, true },
            { FormatterTestModel.error1, null, string.Empty, false },
            { FormatterTestModel.error2, null, string.Empty, false }
        };
    }
}
