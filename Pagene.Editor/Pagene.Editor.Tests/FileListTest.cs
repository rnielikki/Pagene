using Pagene.Editor;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using System.Collections.Generic;
using System.IO.Abstractions;
using System;
using System.Linq;

namespace Pagene.Editor.Tests
{
    public class FileListTest
    {
        [Fact]
        public void GetPostListTest()
        {
            const string newerFile = "bbb.md";
            const string subject1 = "placeholder aaa";
            const string subject2 = "can I test you";
            const string subjectFromRejected = "hello world";
            IFileSystem fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { "inputs/contents/adsf.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# {subject1}{Environment.NewLine}x") },
                    { "inputs/contents/aaa.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# {subject2}{Environment.NewLine}y") },
                    { $"inputs/contents/{newerFile}", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# 123123{Environment.NewLine}y") },
                    { "inputs/contents/ccc.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# 234234{Environment.NewLine}y") },
                    { "inputs/contents/meh/xxx.md", new MockFileData($"[tag1, tag2, tag3]{Environment.NewLine}# {subjectFromRejected}{Environment.NewLine}y") }
                }
            );
            var filePaths = fileSystem.DirectoryInfo.FromDirectoryName("inputs/contents").GetFiles();
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
            var postLoader = new BlogPostLoader(fileSystem);
            var posts = postLoader.LoadPosts();
            var files = posts.Select(post => post.FilePath);
            var titles = posts.Select(post => post.Title);
            Assert.Contains(subject1, titles);
            Assert.Contains(subject2, titles);
            Assert.DoesNotContain(subjectFromRejected, titles);
            Assert.Equal(newerFile, files.FirstOrDefault());
        }
    }
}
