using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using Pagene.BlogSettings;

namespace Pagene.Editor.Tests
{
    public class FileNameTest
    {
        [Fact]
        public void FileNameCreationTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { AppPathInfo.InputPath, new MockDirectoryData()},
                    { AppPathInfo.BlogInputPath, new MockDirectoryData()}
                }
            );
            NamingLogic namer = new NamingLogic(fileSystem);
            var result = namer.GetName("Tämä on testI\\");
            var shouldBe = "tama-on-testi.md";
            Assert.Equal(shouldBe, result);
            fileSystem.File.WriteAllBytes(AppPathInfo.BlogInputPath+"tama-on-testi.md", new byte[] { });
            fileSystem.File.WriteAllBytes(AppPathInfo.BlogInputPath+"tama-on-testi-0.md", new byte[] { });
            fileSystem.File.WriteAllBytes(AppPathInfo.BlogInputPath+"tama-on-testi-1.md", new byte[] { });
            var duplicatedResult = namer.GetName("Tämä on testI\\");
            Assert.Equal("tama-on-testi-2.md", duplicatedResult);
        }
        [Fact]
        public void SpecialNameTest() {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>(){
                    { AppPathInfo.InputPath, new MockDirectoryData()},
                    { AppPathInfo.BlogInputPath, new MockDirectoryData()}
                }
            );
            NamingLogic namer = new NamingLogic(fileSystem);
            var result = namer.GetName("c sharp#");
            Assert.Equal("c-sharp_23.md",result);
        }
    }
}
