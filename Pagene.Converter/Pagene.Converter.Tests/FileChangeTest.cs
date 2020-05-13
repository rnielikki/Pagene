using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System.Text;

namespace Pagene.Converter.Tests
{
    public class FileChangeTest
    {
        private const string testpath = "testpath";
        [Fact]
        public async System.Threading.Tasks.Task FileChangeDetectTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { @$"{testpath}\immutable.md", new MockFileData("not changed!") },
                    { @$"{testpath}\mutable.md", new MockFileData("this will be changed") },
                    { @$"{testpath}\willchange.md", new MockFileData("wait until changed.") },
                    { @$"{testpath}\notchange.md", new MockFileData("should not edited.") }
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, testpath);
            Assert.Equal(fileSystem.AllFiles.OrderBy(str=>str), changeDetector.DetectAsync().Select(file=>file.FullName).OrderBy(str=>str).ToEnumerable());

            using (System.IO.Stream writeStream = fileSystem.FileInfo.FromFileName(@$"{testpath}\mutable.md").OpenWrite())
            {
                await writeStream.WriteAsync(Encoding.ASCII.GetBytes("whatever"));
            }

            fileSystem.AddFile(@$"{testpath}\newfile.md", new MockFileData("hello world, this is new file!"));
            fileSystem.AddFile(@"xdir\newfaile.md", new MockFileData("This shouldn't be on the list"));
            fileSystem.FileInfo.FromFileName(@$"{testpath}\willchange.md").Delete();
            changeDetector = new ChangeDetector(fileSystem, testpath); // regeneration
            Assert.Equal(new string[] { @$"mutable.md", @$"newfile.md" }, changeDetector.DetectAsync().Select(file=>file.Name).OrderBy(str=>str).ToEnumerable());
        }
        [Fact]
        public void HashRemoveTest()
        {
            string filePath = @$"{testpath}\willRemoved.md";
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { filePath, new MockFileData("will removed!") },
                }
            );
            var changeDetector = new ChangeDetector(fileSystem, testpath);
            _ =  changeDetector.DetectAsync();
            fileSystem.FileInfo.FromFileName(filePath).Delete();
            _ = changeDetector.DetectAsync();
            Assert.False(fileSystem.FileExists(filePath));
        }
    }
}
