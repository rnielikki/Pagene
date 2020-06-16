using static Pagene.Converter.Tests.Models.FormatterTestModel;
using Xunit;
using Pagene.BlogSettings;

namespace Pagene.Converter.Tests
{
    public class MainConverterTest
    {
        [Fact]
        public async System.Threading.Tasks.Task MainTest()
        {
            var fileSystem = ValidFileSystem;
            var converter = new Converter(fileSystem);
            await converter.BuildAsync().ConfigureAwait(false);
            Assert.Equal(2, fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogContentPath).GetFiles().Length);
            await converter.BuildAsync().ConfigureAwait(false);
        }
    }
}
