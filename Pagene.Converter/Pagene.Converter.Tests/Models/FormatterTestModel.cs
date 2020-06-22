using Pagene.BlogSettings;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace Pagene.Converter.Tests.Models
{
    internal static class FormatterTestModel
    {
        public const string title1 = "title";
        public const string title2 = "random to say";
        private static readonly string content1 = @$"[{string.Join(", ", TagsTestModel.tags1Duplicated)}]
# {title1}
content";
        private static readonly string content2 = @$"[{string.Join(", ", TagsTestModel.tags2)}]
# {title2}
I have nothing.";

        private const string errorContent1 = @"[bread,cheese,milk]
this one has no title.";

        private const string errorContent2 = @"# I have no tag

This should thorw an error.";

        internal static readonly string InputContentPath = System.IO.Path.Combine(AppPathInfo.InputPath, AppPathInfo.ContentPath);
        internal static readonly string OutputContentPath = System.IO.Path.Combine(AppPathInfo.OutputPath, AppPathInfo.ContentPath);
        private static readonly string filePath1 = InputContentPath + "test.md";
        private static readonly string filePath2 = InputContentPath + "test2.md";
        private static readonly string errorPath1 = InputContentPath + "err1.md";
        private static readonly string errorPath2 = InputContentPath + "err2.md";
        internal static readonly MockFileSystem fileSystem = new MockFileSystem(
             new Dictionary<string, MockFileData>(){
                    { filePath1, new MockFileData(content1) },
                    { filePath2, new MockFileData(content2) },
                    { errorPath1, new MockFileData(errorContent1) },
                    { errorPath2, new MockFileData(errorContent2) },
             }
         );

        internal static readonly IFileInfo file1 = fileSystem.FileInfo.FromFileName(filePath1);
        internal static readonly IFileInfo file2 = fileSystem.FileInfo.FromFileName(filePath2);
        internal static readonly IFileInfo error1 = fileSystem.FileInfo.FromFileName(errorPath1);
        internal static readonly IFileInfo error2 = fileSystem.FileInfo.FromFileName(errorPath2);

        //end test
        internal static readonly MockFileSystem ValidFileSystem = new MockFileSystem(
             new Dictionary<string, MockFileData>(){
                    { filePath1, new MockFileData(content1) },
                    { filePath2, new MockFileData(content2) }
             }
         );
    }
}
