using Pagene.BlogSettings;
using Pagene.Models;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Utf8Json;
using Xunit;

namespace Pagene.Converter.Tests
{
    public class TagSerializationTest
    {
        [Fact]
        public async System.Threading.Tasks.Task MetaCountTest()
        {
            var fileSystem = new MockFileSystem(
                new Dictionary<string, MockFileData>() {
                    { AppPathInfo.BlogTagPath, new MockDirectoryData() }
                }
            );
            var tagManager = new TagManager(fileSystem);
            tagManager.AddTag(new string[] { "csharp", "fsharp", "qsharp" }, new BlogEntry { Url = "a"});
            tagManager.AddTag(new string[] { "csharp", "fsharp", "jsharp" }, new BlogEntry { Url = "b"});
            tagManager.AddTag(new string[] { "csharp", "cplusplus", "cminusminus" }, new BlogEntry { Url = "c"});
            await tagManager.Serialize();
            using var stream = fileSystem.File.Open(AppPathInfo.BlogTagPath + "meta.tags.json", System.IO.FileMode.Open);
            var result = await JsonSerializer.DeserializeAsync<Dictionary<string, TagMeta>>(stream);
            Assert.Equal(1, result["qsharp"].Count);
            Assert.Equal(2, result["fsharp"].Count);
            Assert.Equal(3, result["csharp"].Count);
        }
    }
}
