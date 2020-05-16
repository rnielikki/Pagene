using Xunit;
using System.Threading.Tasks;
using System.IO;

namespace Pagene.Converter.Tests
{
    public class FileChangeTest
    {
        // ------ Blog post test
        [Fact]
        public async Task FileChangeDetectTest()
        {

            using var hashAlgorithm = System.Security.Cryptography.SHA1.Create();

            using var testStream = new MemoryStream(new byte[] { 1, 2, 3 });
            using var writer = new StreamWriter(testStream);
            using var hashStream = new MemoryStream(hashAlgorithm.ComputeHash(new byte[] { 1, 2, 3}));

            var changeDetector = new ChangeDetector(hashAlgorithm);
            Assert.Null(await changeDetector.DetectAsync(file: testStream, hash: hashStream));

            testStream.Position = 0;
            await writer.WriteAsync('.');
            await writer.FlushAsync();
            Assert.NotNull(await changeDetector.DetectAsync(file: testStream, hash: hashStream));
        }
    }
}
