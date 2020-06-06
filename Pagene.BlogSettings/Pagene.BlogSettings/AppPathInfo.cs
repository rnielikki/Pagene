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
        public const string InputPath = "inputs/";
        /// <summary>
        /// Output folder root, which contains all converted contens and tag files.
        /// </summary>
        /// <note>This will be used for separating input files from converted files in the future.</note>
        public const string OutputPath = "";
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
        /// Path, which cRelative to main program (or user-defined) path.ontains unconverted blog posts. (*.md format) Relative to main program (or user-defined) path.
        /// </summary>
        /// <note>Converting from subdirectory of this directory doesn't work.</note>
        public static readonly string BlogInputPath = Path.Combine(InputPath, ContentPath);
        /// <summary>
        /// Attachment file path relative to the output path.
        /// Can contain not only pictures, but texts, documents, codes and anything etc.
        /// </summary>
        public static readonly string BlogFilePath = Path.Combine(OutputPath, ContentPath, FilePath);
        /// <summary>
        /// Content file path relative to the output path.
        /// </summary>
        public static readonly string BlogContentPath = Path.Combine(OutputPath, ContentPath);
        /// <summary>
        /// Entry file path relative to the output path.
        /// </summary>
        public static readonly string BlogEntryPath = Path.Combine(OutputPath, EntryPath);
        /// <summary>
        /// Tag path. Rlative to main program (or user-defined) path.
        /// </summary>
        public static readonly string BlogTagPath = Path.Combine(BlogEntryPath, TagPath);
        /// <summary>
        /// Hash file Path. Hash stores each file hash to check if the file has changed.
        /// </summary>
        public static readonly string BlogHashPath = Path.Combine(InputPath, HashPath);
    }
}
