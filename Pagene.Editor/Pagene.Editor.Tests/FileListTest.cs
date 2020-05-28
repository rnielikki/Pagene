using Xunit;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO.Abstractions;
using System;
using System.Linq;
using Moq;
using Pagene.Reader.PostSerializer;
using System.IO;
using Pagene.Models;
using Pagene.BlogSettings;

namespace Pagene.Editor.Tests
{
    public class FileListTest
    {
        [Fact]
        public void GetPostListTest()
        {
            const string newerFile = "bbb.md";
            const string fileName = "placeholder.md";
            IFileSystem fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { AppPathInfo.BlogInputPath + fileName, new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# x{Environment.NewLine}x") },
                    { $"{AppPathInfo.BlogInputPath}asdfadsf.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# y{Environment.NewLine}y") },
                    { AppPathInfo.BlogInputPath + newerFile, new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# 123123{Environment.NewLine}y") },
                    { $"{AppPathInfo.BlogInputPath}test.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# 234234{Environment.NewLine}y") },
                }
            );
            var filePaths = fileSystem.DirectoryInfo.FromDirectoryName(AppPathInfo.BlogInputPath).GetFiles();
            int day=1;
            foreach (var file in filePaths)
            {
                if (file.Name == newerFile)
                {
                    file.CreationTimeUtc = new DateTime(2020,10,2);
                }
                else
                {
                    file.CreationTimeUtc = new DateTime(2015,10,day);
                }
                day += 4;
            }
            var serializerMock = new Mock<IPostSerializer>();
            serializerMock.Setup(obj => obj.DeserializeAsync(It.IsAny<Stream>())).ReturnsAsync(new BlogItem());
            var postLoader = new BlogPostLoader(fileSystem, serializerMock.Object);
            var files = postLoader.LoadPosts().Select(post => post.FileName);
            Assert.Contains(fileName, files);
            Assert.Equal(newerFile, files.FirstOrDefault());
        }
    }
}
