using Xunit;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using Moq;
using System;
using Pagene.Models;
using System.Linq;

namespace Pagene.Converter.Tests
{
    public class RecentPostsTest
    {
        [Fact]
        public async System.Threading.Tasks.Task RecentPostSerializingTest()
        {
            var mockFormatter = new Mock<IFormatter>();
            mockFormatter.Setup(formatter => formatter.GetBlogEntryAsync(It.IsAny<IFileInfo>()))
                .ReturnsAsync((IFileInfo fileInfo) => new BlogEntry
                {
                    Title = "",
                    Date = fileInfo.CreationTime,
                    Url = fileInfo.Name,
                    Tags = new string[] { }
                }
            );
            IFileSystem fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { Models.FormatterTestModel.InputContentPath+"five.md", new MockFileData("2005/11/12") },
                    { Models.FormatterTestModel.InputContentPath+ "six.md", new MockFileData("2005/10/11") },
                    { Models.FormatterTestModel.InputContentPath+ "three.md", new MockFileData("2015/03/11") },
                    { Models.FormatterTestModel.InputContentPath+ "one.md", new MockFileData("2020/01/01") },
                    { Models.FormatterTestModel.InputContentPath+ "four.md", new MockFileData("2011/12/12") },
                    { Models.FormatterTestModel.InputContentPath+ "two.md", new MockFileData("2017/12/17") }
                }
            );
            foreach (IFileInfo file in fileSystem.DirectoryInfo.FromDirectoryName(Models.FormatterTestModel.InputContentPath).GetFiles())
            {
                using var txtReader = file.OpenText();
                string rawDate = txtReader.ReadToEnd();
                file.CreationTime = DateTime.ParseExact(rawDate, "yyyy/M/d", System.Globalization.CultureInfo.InvariantCulture);
            }
            var manager = new RecentPostManager(fileSystem, mockFormatter.Object);
            var recentPosts = await manager.GetRecentPosts(3).ConfigureAwait(false);
            Assert.NotNull(recentPosts);
            Assert.Equal(recentPosts.Select(post => post.Url).ToArray(), new string[] { "one.md", "two.md", "three.md" });
            recentPosts = await manager.GetRecentPosts(10).ConfigureAwait(false);
            Assert.Equal(recentPosts.Select(post => post.Url).ToArray(), new string[] { "one.md", "two.md", "three.md", "four.md", "five.md", "six.md" });
        }
    }
}
