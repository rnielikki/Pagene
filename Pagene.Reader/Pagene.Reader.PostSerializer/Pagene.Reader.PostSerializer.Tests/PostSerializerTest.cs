using Xunit;
using Pagene.Models;
using System.IO;
using System.Linq;

namespace Pagene.Reader.PostSerializer.Tests
{
    public class PostSerializerTest
    {
        private readonly PostSerializer serializer = new PostSerializer();
        [Fact]
        public async System.Threading.Tasks.Task SerializeTest()
        {
            BlogItem item = new BlogItem(
                    title: "123",
                    content: "content",
                    tags: new string[] { "123", "asdf", "aaaaaaaa"}
                );
            using Stream serializedStream = new MemoryStream();
            await serializer.SerializeAsync(item, serializedStream).ConfigureAwait(false);
            StreamReader reader = new StreamReader(serializedStream);
            serializedStream.Position = 0;
            string result = await reader.ReadToEndAsync().ConfigureAwait(false);

            Assert.Equal(@$"[{string.Join(',',item.Tags)}]
# {item.Title}
{item.Content}", result);
        }
        [Fact]
        public async System.Threading.Tasks.Task DeserializeTest()
        {
            const string title = "________";
            const string content = "hjklhjklhjklhjklhjkl";
            string[] tags = { ":P", "X)", "010101"};
            string inputData = @$"[{string.Join(',', tags)}]
# {title}
{content}";
            using Stream inputStream = new MemoryStream();
            inputStream.Write(System.Text.Encoding.UTF8.GetBytes(inputData));
            inputStream.Position = 0;
            BlogItem item = await serializer.DeserializeAsync(inputStream).ConfigureAwait(false);
            Assert.Equal(title, item.Title);
            Assert.Equal(content, item.Content);
            Assert.Equal(tags.OrderBy(str => str), item.Tags.OrderBy(str => str));
        }
    }
}
