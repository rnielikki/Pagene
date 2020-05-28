namespace Pagene.BlogSettings
{
    /// <summary>
    /// File input/output paths. Only relative paths are used.
    /// </summary>
    public static class AppPathInfo
    {
        /// <summary>
        /// Content path for blog posts. This path contains blog posts.
        /// It affaects to <see cref="InputPath"/> too, where post files before converting are.
        /// </summary>
        public const string ContentPath = "contents/";
        /// <summary>
        /// Blog entries for showing lists on certaion conditions, such as list of tags or recent posts.
        /// </summary>
        public const string EntryPath = "entries/";
        /// <summary>
        /// Path of any blog attachments, relative to content path.
        /// Blog post can reference by linkining to the filePath.
        /// </summary>
        public const string FilePath = "files/";
        /// <summary>
        /// Input folder path. Files from this path will be converted.
        /// </summary>
        public const string InputPath = "inputs/";
        /// <summary>
        /// Tag file path. Contains meta.tags.json for the list of tags.
        /// </summary>
        public const string TagPath = "tags/";
        /// <summary>
        /// Hash file Path. Hash stores each file hash to check if the file has changed.
        /// </summary>
        public const string HashPath = ".hash/";

        /// <summary>
        /// Path, which cRelative to main program (or user-defined) path.ontains unconverted blog posts. (*.md format) Relative to main program (or user-defined) path.
        /// </summary>
        /// <note>Converting from subdirectory of this directory doesn't work.</note>
        public static string BlogInputPath { get => $"{InputPath}{ContentPath}"; }
        /// <summary>
        /// Attachment file path relative to the main program (or user-defined) path.
        /// Can contain not only pictures, but texts, documents, codes and anything etc.
        /// </summary>
        public static string BlogFilePath { get => $"{ContentPath}{FilePath}"; }
        /// <summary>
        /// Tag path. Rlative to main program (or user-defined) path.
        /// </summary>
        public static string BlogTagPath { get => $"{EntryPath}{TagPath}"; }
    }
}
