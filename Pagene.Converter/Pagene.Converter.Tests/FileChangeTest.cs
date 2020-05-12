using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System.Text;

namespace Pagene.Converter.Tests
{
    public class FileChangeTest
    {
        [Fact]
        public async System.Threading.Tasks.Task FileChangeDetectTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { @"C:\immutable.txt", new MockFileData("not changed!") },
                    { @"C:\mutable.txt", new MockFileData("this will be changed") },
                    { @"C:\willchange.txt", new MockFileData("wait until changed.") },
                    { @"C:\notchange.txt", new MockFileData("should not edited.") }
                }
            );
            var changeDetector = new ChangeDetector(fileSystem);
            Assert.Equal(fileSystem.AllFiles.OrderBy(str=>str), await changeDetector.DetectAsync().Select(file=>file.FullName).OrderBy(str=>str));

            using (System.IO.Stream writeStream = fileSystem.FileInfo.FromFileName(@"C:\mutable.txt").OpenWrite())
            {
                await writeStream.WriteAsync(Encoding.ASCII.GetBytes("whatever"));
            }

            fileSystem.AddFile(@"C:\dir\newfile.txt", new MockFileData("hello world, this is new file!"));
            fileSystem.FileInfo.FromFileName(@"C:\willchange.txt").Delete();
            Assert.Equal(new string[] { @"C:\mutable.txt", @"C:\dir\newfile.txt", @"C:\willchange.txt" }, await changeDetector.DetectAsync().Select(file=>file.FullName).OrderBy(str=>str));
        }
    }
}
