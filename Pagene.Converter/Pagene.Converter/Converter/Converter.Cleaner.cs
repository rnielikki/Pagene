using Pagene.BlogSettings;

namespace Pagene.Converter
{
    public partial class Converter
    {
        /// <summary>
        /// Cleans all hash (cache).
        /// </summary>
        /// <remarks>This removes the whole hash directory.</remarks>
        public void Clean() => _fileSystem.Directory.Delete(AppPathInfo.BlogHashPath, true);
        /// <summary>
        /// Cleans all outputs and rebuilds from first.
        /// </summary>
        /// <remarks>This does not remove attachment files inside Blog Content.</remarks>
        public async System.Threading.Tasks.Task RebuildAsync()
        {
            Clean();
            CleanFiles(System.IO.Path.Combine(AppPathInfo.OutputPath, AppPathInfo.ContentPath));
            CleanFiles(AppPathInfo.BlogEntryPath);
            CleanFiles(AppPathInfo.BlogTagPath);
            await BuildAsync().ConfigureAwait(false);
        }
        private void CleanFiles(string path)
        {
            var directory = _fileSystem.DirectoryInfo.FromDirectoryName(path);
            foreach (var file in directory.GetFiles("*", System.IO.SearchOption.TopDirectoryOnly))
            {
                file.Delete();
            }
        }
    }
}
