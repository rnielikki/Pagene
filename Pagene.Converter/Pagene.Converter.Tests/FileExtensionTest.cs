using Xunit;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace Pagene.Converter.Tests
{
    public class FileExtensionTest
    {
        [Fact]
        public void OpenWithCreatingDirectoryTest()
        {
            IFileSystem fileSystem = new MockFileSystem();
            string[] paths = new[] { "animals", "cats", "purrfect.png" };
            char separator = System.IO.Path.DirectorySeparatorChar;
            string fullPaths = string.Join(separator, paths);
            
            fileSystem.Directory.CreateDirectoriesIfNotExist(separator.ToString(), fullPaths);
            Assert.True(fileSystem.Directory.Exists($"{separator}animals"));
            Assert.True(fileSystem.Directory.Exists($"{separator}animals{separator}cats"));
            Assert.False(fileSystem.Directory.Exists($"{separator}animals{separator}cats{separator}purrfect.png"));
        }
    }
}
