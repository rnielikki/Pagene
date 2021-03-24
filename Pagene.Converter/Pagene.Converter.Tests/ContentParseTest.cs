using Pagene.BlogSettings;
using Pagene.Converter.FileTypes;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace Pagene.Converter.Tests
{
    public class ContentParseTest
    {
        //Note: possible replacement for future (to one of "text filter pipelines").
        [Theory]
        [MemberData(nameof(ContentTheoryItems))]
        public async System.Threading.Tasks.Task ParsingContentTest(string syntax, string expected)
        {
            Stream syntaxStream = new MemoryStream(Encoding.UTF8.GetBytes(syntax));
            var readContent = typeof(PostFileType).GetMethod("ReadContent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            string result = await (System.Threading.Tasks.Task<string>) readContent.Invoke(null, new[] { new StreamReader(syntaxStream) });
            Assert.Equal(expected, result);
        }
        public static TheoryData<string, string> ContentTheoryItems() => new TheoryData<string, string>()
        {
            {$"t!!![asdfadsf]({AppPathInfo.FilePath}nnn/xxx.png)",  $"t!!![asdfadsf]({AppPathInfo.ContentPath}{AppPathInfo.FilePath}nnn/xxx.png)"},
            {"t!!![asdfadsf](meh/nnn/xxx.png)", "t!!![asdfadsf](meh/nnn/xxx.png)"},
            {$"[linkSyntax]({AppPathInfo.FilePath}/nnn/xxx.png)", $"[linkSyntax]({AppPathInfo.ContentPath}{AppPathInfo.FilePath}/nnn/xxx.png)"}
        };
    }
}
