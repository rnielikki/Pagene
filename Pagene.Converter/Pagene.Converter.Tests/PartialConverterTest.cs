using Xunit;
using Moq;
using Pagene.Converter.FileTypes;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Pagene.BlogSettings;

namespace Pagene.Converter.Tests
{
    public class PartialConverterTest
    {
        private readonly MockFileSystem _fileSystem;
        public PartialConverterTest()
        {
            _fileSystem = new MockFileSystem
            (
                new Dictionary<string, MockFileData>(){
                    { $"{AppPathInfo.InputPath}cake.pdf", new MockFileData("") },
                    { $"{AppPathInfo.InputPath}clippy.png", new MockFileData("") },
                    { $"{AppPathInfo.InputPath}fruit/apple.jpg", new MockFileData("") },
                    { $"{AppPathInfo.InputPath}fruit/strawberry.gif", new MockFileData("") },
                    { $"{AppPathInfo.InputPath}animal/cat/persian.bmp", new MockFileData("") },
                }
            );
        }

        [Fact]
        public async Task NonSubdirectoryConvertingTest()
        {
            var fileTypeMock = BuildFileTypeMock();
            fileTypeMock.SetupGet(f => f.DirectorySearchOption).Returns(SearchOption.TopDirectoryOnly);
            await new PartialConverter(fileTypeMock.Object, _fileSystem).BuildAsync().ConfigureAwait(false);
            fileTypeMock.Verify(f => f.SaveAsync(It.IsAny<IFileInfo>(), It.IsAny<Stream>()), Times.Exactly(2));
        }

        [Fact]
        public async Task SubdirectoryConvertingTest()
        {
            var fileTypeMock = BuildFileTypeMock();
            fileTypeMock.SetupGet(f => f.DirectorySearchOption).Returns(SearchOption.AllDirectories);
            await new PartialConverter(fileTypeMock.Object, _fileSystem).BuildAsync().ConfigureAwait(false);
            fileTypeMock.Verify(f => f.SaveAsync(It.IsAny<IFileInfo>(), It.IsAny<Stream>()), Times.Exactly(5));
        }

        private Mock<FileType> BuildFileTypeMock()
        {
            var fileTypeMock = new Mock<FileType>(MockBehavior.Strict, _fileSystem, "");
            fileTypeMock.SetupGet(f => f.Type).Returns("*");
            fileTypeMock.Setup(f => f.CleanAsync(It.IsAny<IEnumerable<string>>())).Returns(Task.CompletedTask);
            fileTypeMock.Setup(f => f.SaveAsync(It.IsAny<IFileInfo>(), It.IsAny<Stream>())).Returns(Task.CompletedTask);
            return fileTypeMock;
        }
    }
}
