using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace Pagene.Converter.Tests.Models
{
    internal static class FormatterTestModel
    {
        public const string title1 = "title";
        public const string title2 = "random to say";
        private static readonly string content1 = @$"# {title1}
content

[{string.Join(", ", TagsTestModel.tags1Duplicated)}]";
        private static readonly string content2 = @$"# {title2}
I have nothing.
[{string.Join(", ", TagsTestModel.tags2)}]";

        private const string errorContent1 = @"this one has no title.

[bread,cheese,milk]";

        private const string errorContent2 = @"# I have no tag

This should thorw an error.";


        private const string filePath1 = @"home\test.txt";
        private const string filePath2 = @"home\test2.txt";
        private const string errorPath1 = @"home\err1.txt";
        private const string errorPath2 = @"home\err2.txt";
        private static readonly MockFileSystem fileSystem = new MockFileSystem(
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
    }
}
