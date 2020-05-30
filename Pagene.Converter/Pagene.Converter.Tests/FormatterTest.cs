using Pagene.Converter.Tests.Models;
using System.IO.Abstractions;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Pagene.BlogSettings;
using System.IO;
using Moq;
using Pagene.Reader.PostSerializer;
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
            using var fileReader = new StreamReader(sample.Open(FileMode.Open));
            if (!valid)
            {
                await Assert.ThrowsAsync<FormatException>(() => formatter.GetBlogHead(sample, fileReader)).ConfigureAwait(false);
            }
            else
            {
                var entry = await formatter.GetBlogHead(sample, fileReader).ConfigureAwait(false);
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

            var mockFileSystem = new MockFileSystem();
            const string anyFile = "asdf.md";
            var info = new MockFileInfo(mockFileSystem, anyFile);
            mockFileSystem.AddFile(anyFile, new MockFileData(Environment.NewLine+ Environment.NewLine + rawData));
            formatter.SummaryLength = 7;
            var summary = (await formatter.GetBlogEntry(info).ConfigureAwait(false)).Summary;

            Assert.Equal(expected, summary);
        }
        [Theory]
        [InlineData("short", "short")]
        [InlineData("abcdefghijklmnop", "abcdefg")]
        public void SummaryCutTest(string rawData, string expected)
        {
            IFormatter formatter = new Formatter("", new Mock<IFormatParser>().Object)
            {
                SummaryLength = 7
            };
            var summary = formatter.GetSummary(rawData);
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
