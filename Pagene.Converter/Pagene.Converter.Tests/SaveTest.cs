using Xunit;
using System.Threading.Tasks;
using Moq;
using Pagene.Converter.FileTypes;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using Pagene.Models;
using Pagene.BlogSettings;

namespace Pagene.Converter.Tests
{
    public class SaveTest
    {
        [Fact]
        public async Task SaveDefaultTest()
        {
            const string path = "tests";
            string contentPath = $"{path}/something.md";
            const string content = "123123";
            MockFileSystem fileSystem = new MockFileSystem(
                      new Dictionary<string, MockFileData>(){
                    { contentPath, new MockFileData(content) },
                      }
                  );
            var fileInfo = fileSystem.FileInfo.FromFileName(contentPath);
            using var fileStream = fileInfo.Open(System.IO.FileMode.Open);

            var fileMock = new Mock<FileType>(fileSystem, path) { CallBase = true };
            fileMock.SetupGet(obj => obj.Type).Returns("*");
            await fileMock.Object.SaveAsync(fileInfo, fileStream).ConfigureAwait(false);
            Assert.True(fileSystem.FileExists(contentPath));

            string result = await fileSystem.File.ReadAllTextAsync(contentPath).ConfigureAwait(false);
            Assert.Equal(content, result);
        }
        [Fact]
        public async Task SaveFormatTest()
        {
            const string content = "asdfasdf";
            string contentPath = $"{AppPathInfo.ContentPath}something.md";
            string inputContentPath = AppPathInfo.InputPath+contentPath;

            MockFileSystem fileSystem = new MockFileSystem(
                      new Dictionary<string, MockFileData>(){
                    { inputContentPath, new MockFileData(content) },
                      }
                  );
            var dateTime = new System.DateTime(2020, 2, 2);
            var editDateTime = new System.DateTime(2020, 3, 25);
            var fileInfo = fileSystem.FileInfo.FromFileName(inputContentPath);
            using var fileStream = fileInfo.Open(System.IO.FileMode.Open);
            fileInfo.CreationTimeUtc = dateTime;
            fileInfo.LastWriteTimeUtc = editDateTime;

            var formatterMock = new Mock<IFormatter>();

            formatterMock.Setup(obj => obj.GetBlogHead(It.IsAny<System.IO.Abstractions.IFileInfo>(), It.IsAny<System.IO.Stream>()))
                .ReturnsAsync(new BlogEntry { Title = "title", Date = dateTime, Url = contentPath, Tags = new string[] { "book", "game", "music" }});
            var tagManager = new TagManager(fileSystem);
            var fileType = new MdFileType(fileSystem, formatterMock.Object, tagManager);

            await fileType.SaveAsync(fileInfo, fileStream).ConfigureAwait(false);
            string result = await fileSystem.File.ReadAllTextAsync(contentPath).ConfigureAwait(false);
            Assert.Equal(@$"> {dateTime}
> {editDateTime}
{content}", result);
        }
    }
}
