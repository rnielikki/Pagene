using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace Pagene.Editor.Tests
{
    public class FileNameTest
    {
        [Fact]
        public void FileNameCreationTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { "inputs/", new MockDirectoryData()},
                    { "inputs/contents", new MockDirectoryData()}
                }
            );
            NamingLogic namer = new NamingLogic(fileSystem);
            var result = namer.GetName("Tämä on testI\\");
            var shouldBe = "tama-on-testi";
            Assert.Equal(shouldBe, result);
            fileSystem.File.WriteAllBytes("inputs/contents/tama-on-testi.md", new byte[] { });
            fileSystem.File.WriteAllBytes("inputs/contents/tama-on-testi-0.md", new byte[] { });
            fileSystem.File.WriteAllBytes("inputs/contents/tama-on-testi-1.md", new byte[] { });
            var duplicatedResult = namer.GetName("Tämä on testI\\");
            Assert.Equal("tama-on-testi-2", duplicatedResult);
        }
    }
}
