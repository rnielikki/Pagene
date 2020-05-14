using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System.Text;
using Moq;
using System.Threading.Tasks;

namespace Pagene.Converter.Tests
{
    public class FileChangeTest
    {
        private readonly DirSettings dirSettings = new DirSettings();
        private readonly string attachmentDir;
        private readonly string attachmentHashDir;
        public FileChangeTest()
        {
            attachmentDir = $"{dirSettings.ContentDir}\\{dirSettings.AttachmentDir}";
            attachmentHashDir = $"{dirSettings.HashDir}\\{dirSettings.ContentDir}\\{dirSettings.AttachmentDir}";
        }
        // ------ Blog post test
        [Fact]
        public async Task FileChangeDetectTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { @$"{dirSettings.ContentDir}\immutable.md", new MockFileData("not changed!") },
                    { @$"{dirSettings.ContentDir}\mutable.md", new MockFileData("this will be changed") },
                    { @$"{dirSettings.ContentDir}\notchange.md", new MockFileData("should not edited.") }
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, dirSettings);
            Assert.Equal(fileSystem.AllFiles.OrderBy(str=>str), changeDetector.DetectBlogPostAsync().Select(file=>file.FullName).OrderBy(str=>str).ToEnumerable());

            using (System.IO.Stream writeStream = fileSystem.FileInfo.FromFileName(@$"{dirSettings.ContentDir}\mutable.md").OpenWrite())
            {
                await writeStream.WriteAsync(Encoding.ASCII.GetBytes("whatever"));
            }

            fileSystem.AddFile(@$"{dirSettings.ContentDir}\newfile.md", new MockFileData("hello world, this is new file!"));
            fileSystem.AddFile(@"xdir\newfaile.md", new MockFileData("This shouldn't be on the list"));
            changeDetector = new ChangeDetector(fileSystem, dirSettings); // regeneration
            Assert.Equal(new string[] { @$"mutable.md", @$"newfile.md" }, changeDetector.DetectBlogPostAsync().Select(file=>file.Name).OrderBy(str=>str).ToEnumerable());
        }
        [Fact]
        public async Task HashRemoveTest()
        {
            string filePath = @$"{dirSettings.ContentDir}\willRemoved.md";
            string hashPath = @$"{dirSettings.HashDir}\{dirSettings.ContentDir}\willRemoved.md.hashfile";
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { filePath, new MockFileData("will removed!") },
                    { @$"{dirSettings.ContentDir}\mutable.md", new MockFileData("this will be changed") }
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, dirSettings);
            await changeDetector.DetectBlogPostAsync().ToListAsync();
            Assert.True(fileSystem.FileExists(hashPath));

            fileSystem.FileInfo.FromFileName(filePath).Delete();
            changeDetector = new ChangeDetector(fileSystem, dirSettings);
            changeDetector.DetectBlogPostAsync().ToEnumerable();
            await changeDetector.DetectBlogPostAsync().ToListAsync();
            Assert.False(fileSystem.FileExists(hashPath));
        }
        // ------ Attachment test
        [Fact]
        public async Task AttachHashRemoveTest()
        {
            string filePath = @$"{attachmentDir}\reader.pdf";
            var mockObject = Mock.Get(Mock.Of<DirSettings>());
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { filePath, new MockFileData("") },
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, dirSettings);
            await changeDetector.DetectAttachmentsAsync().ToListAsync();
            Assert.True(fileSystem.FileExists($"{attachmentHashDir}\\reader.pdf.hashfile"));

            fileSystem.FileInfo.FromFileName(filePath).Delete();
            changeDetector = new ChangeDetector(fileSystem, dirSettings);
            await changeDetector.DetectAttachmentsAsync().ToListAsync();
            Assert.False(fileSystem.FileExists($"{attachmentHashDir}\\reader.pdf.hashfile"));
        }
        [Fact]
        public async Task AttachFileDetectTestAsync()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { @$"{attachmentDir}\filetype.jpg", new MockFileData("") },
                    { @$"{attachmentDir}\reader.pdf", new MockFileData("") },
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, dirSettings);
            await changeDetector.DetectAttachmentsAsync().ToListAsync();

            fileSystem.AddFile(@$"{attachmentDir}\newfile.png", new MockFileData("my file"));
            using (System.IO.Stream writeStream = fileSystem.FileInfo.FromFileName(@$"{attachmentDir}\filetype.jpg").OpenWrite())
            {
                await writeStream.WriteAsync(Encoding.ASCII.GetBytes("_CHANGED!"));
            }

            changeDetector = new ChangeDetector(fileSystem, dirSettings);
            Assert.Equal(new string[] { "filetype.jpg", "newfile.png" }, changeDetector.DetectAttachmentsAsync().Select(file=>file.Name).OrderBy(str=>str).ToEnumerable());
        }
    }
}
