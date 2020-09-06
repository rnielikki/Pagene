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
        private PostFileType _postFileType;
        public ContentParseTest()
        {
            var mockFileSystem = new MockFileSystem();
            _postFileType = new PostFileType(mockFileSystem, new TagManager(mockFileSystem));
        }
        [Theory]
        [MemberData(nameof(ContentTheoryItems))]
        public async System.Threading.Tasks.Task ParsingContentTest(string syntax, string expected)
        {
            Stream syntaxStream = new MemoryStream(Encoding.UTF8.GetBytes(syntax));
            string result = await _postFileType.ReadContent(new StreamReader(syntaxStream)).ConfigureAwait(false);
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
