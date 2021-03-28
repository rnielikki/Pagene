using Pagene.BlogSettings;
using System.IO.Abstractions;

namespace Pagene.Converter
{
    internal static class InitializationHelper
    {
        /// <summary>
        /// Creates directories if not exist, which needs for converting.
        /// </summary>
        /// <param name="fileSystem">The file system to initialize, which can be mocked or not.</param>
        public static void Initialize(IFileSystem fileSystem)
        {
            InitDirectory(fileSystem, System.IO.Path.Combine(AppPathInfo.InputPath, AppPathInfo.ContentPath));
            InitDirectory(fileSystem, System.IO.Path.Combine(AppPathInfo.OutputPath, AppPathInfo.ContentPath));
            InitDirectory(fileSystem, AppPathInfo.BlogTagPath);
            InitDirectory(fileSystem, AppPathInfo.BlogHashPath);
            InitDirectory(fileSystem, AppPathInfo.BlogFilePath);
        }
        /// <summary>
        /// Creates directory if not exist.
        /// </summary>
        /// <param name="fileSystem">The file system to initialize, which can be mocked or not.</param>
        /// <param name="path">The path of the directory to initialize.</param>
        /// <returns>The directory info from the path.</returns>
        internal static IDirectoryInfo InitDirectory(IFileSystem fileSystem, string path)
        {
            var info = fileSystem.DirectoryInfo.FromDirectoryName(path);
            if (!info.Exists)
            {
                return fileSystem.Directory.CreateDirectory(path);
            }
            else
            {
                return info;
            }
        }
    }
}
