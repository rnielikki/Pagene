using System;
using System.IO.Abstractions;
using System.Linq;
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
        public void TitleValidationTest(IFileInfo sample, string title, bool valid)
        {
            if (!valid)
            {
                Assert.Throws<FormatException>(()=>formatter.Parse(sample));
            }
            else {
                BlogItem result = formatter.Parse(sample);
                Assert.Equal("# "+title, result.Title);
            }
        }

   [Fact]
   public void TagParseTest()
   {
       BlogItem item1 = formatter.Parse(FormatterTestModel.file1);
       BlogItem item2 = formatter.Parse(FormatterTestModel.file2);
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
