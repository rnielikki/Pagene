using static Pagene.Converter.Tests.Models.FormatterTestModel;
using Xunit;

namespace Pagene.Converter.Tests
{
    public class MainConverterTest
    {
        [Fact]
        public async System.Threading.Tasks.Task MainTest()
        {
            var fileSystem = ValidFileSystem;
            var converter = new Converter(fileSystem);
            await converter.Convert();
            Assert.Equal(2, fileSystem.DirectoryInfo.FromDirectoryName("contents").GetFiles().Length);
            Assert.Equal(2, fileSystem.DirectoryInfo.FromDirectoryName("contents\\files").GetFiles().Length);
        }
    }
}
