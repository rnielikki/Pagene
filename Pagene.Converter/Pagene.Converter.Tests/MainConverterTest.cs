using Xunit;
using System.IO;
using Pagene.BlogSettings;
using FluentAssertions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using Pagene.Converter.Tests.Models;

namespace Pagene.Converter.Tests
{
    public class MainConverterTest
    {
        [Fact]
        public async System.Threading.Tasks.Task MainTest()
        {
            const string fileDirName = "bin";
            const string fileName = "Program.cs";
            var fileSystem = new MockFileSystem
            (
                new Dictionary<string, MockFileData>()
                {
                    { GetBlogContentPath("the-cat.md"), new MockFileData(FormatterTestModel.content1) },
                    { GetBlogContentPath("the-dog.md"), new MockFileData(FormatterTestModel.content2) },
                    { GetBlogFilePath(Path.Combine(fileDirName, fileName)), new MockFileData("FILECONTENT") }
                }
            );
            var converter = new Converter(fileSystem);
            converter.Initialize();
            await converter.BuildAsync().ConfigureAwait(false);

            //The input files
            fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogContentPath)
                .GetFiles().Should().HaveCount(2)
                .And.Contain(file => file.Name == "the-cat.json");

            //tag test
            fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogTagPath).GetFiles().Length.Should().BeGreaterThan(2);

            //creates recent posts
            fileSystem.File.Exists(Path.Combine(AppPathInfo.BlogEntryPath, "recent.json")).Should().BeTrue();

            //file copying test
            var fileDirResult = fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogFilePath);
            fileDirResult.GetDirectories().Should().HaveCount(1)
                .And.ContainSingle(file => file.Name == fileDirName);
            fileDirResult.GetDirectories()[0].GetFiles().Should()
                .ContainSingle(file => file.Name == fileName);

            //can call the method again without error
            await converter.BuildAsync().ConfigureAwait(false);
        }
        private string GetBlogContentPath(string pathName) => Path.Combine(AppPathInfo.InputPath, AppPathInfo.ContentPath, pathName);
        private string GetBlogFilePath(string pathName) => Path.Combine(AppPathInfo.InputPath, AppPathInfo.FilePath, pathName);
    }
}
