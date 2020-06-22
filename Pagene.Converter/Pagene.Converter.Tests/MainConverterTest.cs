using static Pagene.Converter.Tests.Models.FormatterTestModel;
using Xunit;
using Pagene.BlogSettings;
using FluentAssertions;

namespace Pagene.Converter.Tests
{
    public class MainConverterTest
    {
        [Fact]
        public async System.Threading.Tasks.Task MainTest()
        {
            var fileSystem = ValidFileSystem;
            var converter = new Converter(fileSystem);
            converter.Initialize();
            await converter.BuildAsync().ConfigureAwait(false);
            fileSystem.DirectoryInfo.FromDirectoryName(System.IO.Path.Combine(AppPathInfo.OutputPath, AppPathInfo.ContentPath)).GetFiles().Length.Should().Be(2);
            fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogTagPath).GetFiles().Length.Should().BeGreaterThan(1);
            fileSystem.File.Exists(System.IO.Path.Combine(AppPathInfo.BlogEntryPath, "recent.json")).Should().BeTrue();
            await converter.BuildAsync().ConfigureAwait(false);
        }
    }
}
