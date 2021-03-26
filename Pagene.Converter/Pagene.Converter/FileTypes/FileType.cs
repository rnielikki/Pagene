using System.IO.Abstractions;
using System.IO;
using System.Collections.Generic;
using Pagene.BlogSettings;
using System.Threading.Tasks;

namespace Pagene.Converter.FileTypes
{
    /// <summary>
    /// Represents the collection of converting type definition class.</param>
    /// </summary>
    internal abstract class FileType
    {
        /// <summary>
        /// The path to read file, relative to <see cref="AppPathInfo.InputPath"/> for input and <see cref="AppPathInfo.OutputPath"/> for output.
        /// </summary>
        internal string FilePath { get; }

        /// <summary>
        /// The file extension to process.
        /// </summary>
        internal virtual string Extension { get; }

        protected readonly IFileSystem _fileSystem;

        /// <summary>
        /// If <see cref="SearchOption.AllDirectories"/>, it applies to subdirectory. Otherwise, it only get files from top directory.
        /// </summary>
        public virtual SearchOption DirectorySearchOption { get; protected set; } = SearchOption.AllDirectories;

        /// <summary>
        /// Constructor the converting type defintiion class.
        /// </summary>
        /// <param name="fileSystem">The file system to apply, that is mocked or not.</param>
        /// <param name="path">The path.</param>
        protected FileType(IFileSystem fileSystem, string path)
        {
            FilePath = path;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Saves data to <see cref="FilePath"/>. May contain some logic to change file.
        /// </summary>
        /// <param name="targetFileInfo">The destination (output) <see cref="IFileInfo"/>.</param>
        /// <param name="sourceStream">The source stream that contains data.</param>
        /// <remarks>The default implementation sets sourceStream to 0.</remarks>
        internal virtual async Task SaveAsync(IFileInfo targetFileInfo, Stream sourceStream)
        {
            sourceStream.Position = 0;
            string relativeFilePath;

            if (DirectorySearchOption == SearchOption.AllDirectories)
            {
                relativeFilePath = Path.GetRelativePath(_fileSystem.Path.GetFullPath(AppPathInfo.InputPath), targetFileInfo.FullName);
            }
            else
            {
                relativeFilePath = targetFileInfo.Name;
            }

            Stream writeTarget = GetFileStream(relativeFilePath);
            try
            {
                await sourceStream.CopyToAsync(writeTarget).ConfigureAwait(false);
            }
            finally
            {
                writeTarget.Close();
            }
        }

        protected Stream GetFileStream(string fileName)
        {
            return _fileSystem.File.Open(GetOutputPath(fileName), FileMode.Create);
        }

        protected virtual string GetOutputPath(string fileName)
        {
            _fileSystem.Directory.CreateDirectoriesIfNotExist(AppPathInfo.OutputPath, fileName);
            return Path.Combine(AppPathInfo.OutputPath, fileName);
        }

        /// <summary>
        /// Cleans output files and hash. This exists for cleaning files that doesn't exist anymore in input.
        /// </summary>
        /// <param name="files">Thie files to delete.</param
        internal virtual Task CleanAsync(IEnumerable<string> files)
        {
           foreach (var fileName in files)
            {
                _fileSystem.File.Delete(GetOutputPath(fileName));
                _fileSystem.File.Delete(Path.Combine(AppPathInfo.BlogHashPath, FilePath, fileName+".hashfile"));
            }
            return Task.CompletedTask;
        }
    }
}
