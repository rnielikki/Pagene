using System.IO;

namespace Pagene.BlogSettings
{
    /// <summary>
    /// File input/output paths. Only relative paths are used.
    /// </summary>
    public static class AppPathInfo
    {
        /// <summary>
        /// Input folder path name. Files from this path will be converted.
        /// </summary>
        /// <note>This will be used for separating input files from converted files in the future.</note>
        public static string InputPath { get; internal set; } = "inputs/";
        /// <summary>
        /// Output folder root, which contains all converted contens and tag files.
        /// </summary>
        /// <note>This will be used for separating input files from converted files in the future.</note>
        public static string OutputPath { get; internal set; } = "";
        /// <summary>
        /// Content path name for blog posts. This path contains blog posts.
        /// It affaects to <see cref="InputPath"/> too, where post files before converting are.
        /// </summary>
        public const string ContentPath = "contents/";
        /// <summary>
        /// Blog entries for showing lists on certaion conditions, such as list of tags or recent posts.
        /// </summary>
        public const string EntryPath = "entries/";
        /// <summary>
        /// Path name of any blog attachments, relative to content path.
        /// Blog post can reference by linkining to the filePath.
        /// </summary>
        public const string FilePath = "files/";
        /// <summary>
        /// Tag file path name, inside entry path. Contains tag files as number and meta.tags.json for the list of tags.
        /// </summary>
        public const string TagPath = "tags/";
        /// <summary>
        /// Hash file path name. Hash stores each file hash to check if the file has changed.
        /// </summary>
        public const string HashPath = ".hash/";

        /// <summary>
        /// Path, which is relative to main program (or user-defined) path. Contains unconverted blog posts. (*.md format) Relative to main program (or user-defined) path.
        /// </summary>
        /// <note>Converting from subdirectory of this directory doesn't work.</note>
        public static string BlogInputPath { get => Path.Combine(InputPath, ContentPath); }
        /// <summary>
        /// Attachment file path relative to the output path.
        /// Can contain not only pictures, but texts, documents, codes and anything etc.
        /// </summary>
        public static string BlogFilePath { get => Path.Combine(OutputPath, ContentPath, FilePath); }
        /// <summary>
        /// Content file path relative to the output path.
        /// </summary>
        public static string BlogContentPath { get => Path.Combine(OutputPath, ContentPath); }
        /// <summary>
        /// Entry file path relative to the output path.
        /// </summary>
        public static string BlogEntryPath { get => Path.Combine(OutputPath, EntryPath); }
        /// <summary>
        /// Tag path. Rlative to main program (or user-defined) path.
        /// </summary>
        public static string BlogTagPath { get => Path.Combine(BlogEntryPath, TagPath); }
        /// <summary>
        /// Hash file Path. Hash stores each file hash to check if the file has changed.
        /// </summary>
        public static string BlogHashPath { get => Path.Combine(InputPath, HashPath); }
    }
}
