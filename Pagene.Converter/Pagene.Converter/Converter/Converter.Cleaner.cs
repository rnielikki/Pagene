using Pagene.BlogSettings;

namespace Pagene.Converter
{
    public partial class Converter
    {
        /// <summary>
        /// Cleans all hash (cache).
        /// <remarks>This removes the whole hash directory.</remarks>
        /// </summary>
        public void Clean() {
            _fileSystem.Directory.Delete(AppPathInfo.BlogHashPath, true);
        }
    }
}
