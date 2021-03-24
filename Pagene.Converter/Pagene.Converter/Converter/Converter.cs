using Pagene.Converter.FileTypes;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Pagene.BlogSettings;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Pagene.Converter.Tests")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Pagene.Converter
{
    /// <summary>
    /// Read and generate post and tag list from certain format and certain path.
    /// </summary>
    public partial class Converter
    {
        private readonly IFileSystem _fileSystem;
        internal Converter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        /// <summary>
        /// Creates instance for converting.
        /// </summary>
        public Converter() : this(new FileSystem())
        {
            AppConfigLoader.LoadConfig();
        }

        /// <summary>
        /// Start converting data.
        /// </summary>
        /// <remarks>See other documentation page about converting format and file path.</remarks>
        public async Task BuildAsync()
        {
            var mdFileType = new PostFileType(_fileSystem);
            var attachmentType = new AttachmentFileType(_fileSystem);
            await Task.WhenAll(
                    new[] {
                        new PartialConverter(mdFileType, _fileSystem).BuildAsync(),
                        new PartialConverter(attachmentType, _fileSystem).BuildAsync()
                    }
                ).ConfigureAwait(false);
        }
    }
}
