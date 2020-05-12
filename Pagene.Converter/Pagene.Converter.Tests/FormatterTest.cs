using System;
using System.IO.Abstractions;
using Pagene.Converter.Tests.Models;
using Pagene.Models;
using Xunit;

namespace Pagene.Converter.Tests
{
    public class FormatterTest
    {
        private readonly Formatter formatter = new Formatter();

        [Theory]
        [MemberData(nameof(GetValidationSet))]
        public async System.Threading.Tasks.Task TitleValidationTest(IFileInfo sample, string title, bool valid)
        {
            if (!valid)
            {
                await Assert.ThrowsAsync<FormatException>(formatter.ParseAsync(sample));
            }
            else {
                BlogItem result = await formatter.ParseAsync(sample);
                Assert.Equal(result.Title, title);
            }
        }

        [Theory]
        [MemberData(nameof(DateSamples))]
        public async System.Threading.Tasks.Task DateFormatterTest(DateTime creationDate, DateTime modificationDate)
        {
            FormatterTestModel.file1.CreationTimeUtc = creationDate;
            FormatterTestModel.file2.LastWriteTimeUtc = modificationDate;
            BlogItem parsed = await formatter.ParseAsync(FormatterTestModel.file1);
            Assert.Equal(creationDate, parsed.CreationDate);
            Assert.Equal(modificationDate, parsed.ModificationDate);
        }
        [Fact]
        public async System.Threading.Tasks.Task TagParseTest()
        {
            BlogItem item1 = await formatter.ParseAsync(FormatterTestModel.file1);
            BlogItem item2 = await formatter.ParseAsync(FormatterTestModel.file2);
            Assert.Equal(TagsTestModel.tags1, item1.Tags.OrderBy(str=>str));
            Assert.Equal(TagsTestModel.tags2, item2.Tags.OrderBy(str=>str));
        }

        public static TheoryData<DateTime, DateTime> DateSamples() => new TheoryData<DateTime, DateTime>() {
            { new DateTime(2015, 10, 11), new DateTime(2015, 11, 11) },
            { new DateTime(2016, 3, 12), new DateTime(2016, 3, 12) }
        };
        public static TheoryData<IFileInfo, string, bool> GetValidationSet() => new TheoryData<IFileInfo, string, bool>() {
            { FormatterTestModel.file1, FormatterTestModel.title1, true },
            { FormatterTestModel.file2, FormatterTestModel.title2, true },
            { FormatterTestModel.error1, string.Empty, false },
            { FormatterTestModel.error2, string.Empty, false }
        };
    }
}
