using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    internal static class FilesExtensions
    {
        /// <summary>
        /// Creates with directory.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>The opened/created file stream from the file.</returns>
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
    }
}
