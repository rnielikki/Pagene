using Pagene.Models;
using System.IO;
using Xunit;
using Moq;
using Pagene.Reader.PostSerializer;
using System;

namespace Pagene.Reader.Tests
{
    public class BlogPostReadTest
    {
        [Fact]
        public async System.Threading.Tasks.Task BlogItemReadTest()
        {
            DateTime creationDate = new DateTime(2017, 9, 25);
            DateTime editDate = new DateTime(2019, 11, 30);
            string testInput = @$"> {creationDate}
> {editDate}
anyContent";
            var serializerMock = new Mock<IPostSerializer>();
            serializerMock.Setup(obj => obj.DeserializeAsync(It.IsAny<Stream>()))
                .ReturnsAsync(new BlogItem(It.IsAny<string>(), It.IsAny<string>(), new string[] { }));
            Reader reader = new Reader(serializerMock.Object);
            using MemoryStream stream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(stream);
            await writer.WriteAsync(testInput).ConfigureAwait(false);
            await writer.FlushAsync().ConfigureAwait(false);
            stream.Position = 0;
            BlogItem item = await reader.ReadPostAsync(stream).ConfigureAwait(false);
            Assert.Equal(creationDate, item.CreationDate);
            Assert.Equal(editDate, item.ModificationDate);
        }
    }
}
