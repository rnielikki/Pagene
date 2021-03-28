using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    internal static class FilesExtensions
    {
        /// <summary>
        /// Creates directories in all <c>path</c> parameter, if not exists. If the path itself exists, this does nothing.
        /// </summary>
        /// <param name="directory">The <see cref="IDirectory" />, just for extended method.</param>
        /// <param name="basePath">The whole absolute path base.</param>
        /// <param name="path">Relative the *whoel file path* to create folder if not exist.</param>
        /// <returns>The opened/created file stream from the file.</returns>
        /// <remarks>Regardless of <c>path</c> type, it'll not create directory of the final path. For example, <c>aaa\\bb\\cc</c> creates only <c>aaa\\</c> and <c>aaa\\bb\\</c>.</remarks>
        /// <remarks>This may not effective for multiple directories, but it's not the common scenario.</remarks>
        internal static void CreateDirectoriesIfNotExist(this IDirectory directory, string basePath, string path)
        {
            if (!path.Contains(Path.DirectorySeparatorChar)) return;

            var paths = path.Split(Path.DirectorySeparatorChar).Where(c => !string.IsNullOrEmpty(c)).ToArray();

            for (int i = 1; i < paths.Length; i++)
            {
                string fullPath = Path.Combine(basePath, Path.Combine(paths[0..i]));
                if (!directory.Exists(fullPath))
                {
                    directory.CreateDirectory(fullPath);
                }
            }
        }
        /// <summary>
        /// Calls <see cref="CreateDirectoriesIfNotExist(IDirectory, string, string)"/>, but gets path parameter as absolute value.
        /// </summary>
        /// <param name="directory">The <see cref="IDirectory" />, just for extended method.</param>
        /// <param name="basePath">The whole absolute path base.</param>
        /// <param name="fullPath">Absolute path to create folder if not exists.</param>
        /// <returns>The opened/created file stream from the file.</returns>
        /// <remarks>See <see cref="CreateDirectoriesIfNotExist(IDirectory, string, string)"/> to understand how it works.</remarks>
        internal static void CreateDirectoriesFromFullPath(this IDirectory directory, string basePath, string fullPath) =>
            CreateDirectoriesIfNotExist(
                directory,
                basePath,
                Path.GetRelativePath(directory.FileSystem.Path.GetFullPath(basePath), fullPath)
                );

        /// <summary>
        /// Get relative path to the <c>basePath</c>. This will also create directories if not exists.
        /// </summary>
        /// <param name="fileInfo">The base <see cref="IFileInfo"/>, which contains full path.</param>
        /// <param name="basePath">Full base path to be relative.</param>
        /// <param name="directorySearchOption">If it's <see cref="SearchOption.TopDirectoryOnly"/>, this would return just fileInfo.</param>
        /// <returns>Relative file path as <c>string</c></returns>
        internal static string GetRelativeName(this IFileInfo fileInfo, string basePath, SearchOption directorySearchOption)
        {
            if (directorySearchOption == SearchOption.TopDirectoryOnly) return fileInfo.Name;

            var fileSystem = fileInfo.FileSystem;
            fileSystem.Directory.CreateDirectoriesFromFullPath(basePath, fileInfo.FullName);
            return Path.GetRelativePath(basePath, fileInfo.FullName);
        }
    }
}
