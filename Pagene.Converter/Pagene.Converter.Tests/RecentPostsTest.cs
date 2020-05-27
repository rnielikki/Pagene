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
            mockFormatter.Setup(formatter => formatter.GetBlogHead(It.IsAny<IFileInfo>()))
                .ReturnsAsync((IFileInfo fileInfo) => new BlogEntry
                {
                    Title = "",
                    Date = fileInfo.CreationTimeUtc,
                    URL = fileInfo.Name,
                    Tags = new string[] { }
                }
            );
            IFileSystem fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { "inputs/contents/five.md", new MockFileData("2005/11/12") },
                    { "inputs/contents/six.md", new MockFileData("2005/10/11") },
                    { "inputs/contents/three.md", new MockFileData("2015/03/11") },
                    { "inputs/contents/one.md", new MockFileData("2020/01/01") },
                    { "inputs/contents/four.md", new MockFileData("2011/12/12") },
                    { "inputs/contents/two.md", new MockFileData("2017/12/17") }
                }
            );
            foreach (IFileInfo file in fileSystem.DirectoryInfo.FromDirectoryName("inputs/contents").GetFiles())
            {
                using var txtReader = file.OpenText();
                string rawDate = txtReader.ReadToEnd();
                file.CreationTimeUtc = DateTime.ParseExact(rawDate, "yyyy/M/d", System.Globalization.CultureInfo.InvariantCulture);
            }
            var manager = new RecentPostManager(fileSystem, mockFormatter.Object);
            var recentPosts = await manager.GetRecentPosts(3);
            Assert.Equal(recentPosts.Select(post => post.URL).ToArray(), new string[] { "one.md", "two.md", "three.md" });
            recentPosts = await manager.GetRecentPosts(10);
            Assert.Equal(recentPosts.Select(post => post.URL).ToArray(), new string[] { "one.md", "two.md", "three.md", "four.md", "five.md", "six.md" });
        }
    }
}
