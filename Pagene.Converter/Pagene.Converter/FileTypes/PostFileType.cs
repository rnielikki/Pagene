using Pagene.Models;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using Pagene.BlogSettings;
using Utf8Json;
using System.Text;
using System;

namespace Pagene.Converter.FileTypes
{
    internal class PostFileType : FileType
    {
        internal override string Type => "*.md";
        internal static string OutputType => ".json";
        private readonly IFormatter _formatter;
        private readonly TagManager _tagManager;

        private bool modified;

        internal PostFileType(IFileSystem fileSystem, IFormatter formatter, TagManager tagManager) : base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = formatter;
            _tagManager = tagManager;
            DirectorySearchOption = SearchOption.TopDirectoryOnly;
        }

        internal PostFileType(IFileSystem fileSystem) : base(fileSystem, AppPathInfo.ContentPath)
        {
            _formatter = new Formatter();
            _tagManager = new TagManager(fileSystem);
        }

        internal override async System.Threading.Tasks.Task SaveAsync(IFileInfo targetFileInfo, Stream sourceStream)
        {
            modified = true;
            sourceStream.Position = 0;
            using StreamReader reader = new StreamReader(sourceStream);
            BlogEntry entry = await _formatter.GetBlogHeadAsync(targetFileInfo, reader).ConfigureAwait(false);
            BlogItem item = new BlogItem { Title = entry.Title,
                Content = await ReadContent(reader).ConfigureAwait(false),
                CreationDate = entry.Date,
                ModificationDate = GetModificationDate(targetFileInfo, entry.Date),
                Tags = entry.Tags };
            entry.Summary = _formatter.GetSummary(item.Content);
            using MemoryStream resultStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(resultStream, item).ConfigureAwait(false);
            resultStream.Position = 0;
            await base.SaveAsync(targetFileInfo, resultStream).ConfigureAwait(false);

            //generate categories
            _tagManager.AddTag(entry.Tags, entry);
        }
        internal override async System.Threading.Tasks.Task CleanAsync(IEnumerable<string> files)
        {
            if (!modified) return;

            await base.CleanAsync(files).ConfigureAwait(false);
            _tagManager.Clean(files);

            await _tagManager.Serialize().ConfigureAwait(false);
            var postManager = new RecentPostManager(_fileSystem, _formatter);
            await postManager.Serialize(await postManager.GetRecentPosts(ConvertingInfo.RecentPostsCount).ConfigureAwait(false)).ConfigureAwait(false);
        }

        //Represents logic to change "files" to "contents/files"
        //If you use Visual Studio Code to edit *.md file or GitHub to public editing *.md file, this is useful.

        //Note: possible replacement for future (to one of "text filter pipelines").
        //related test: ContentParseTest.cs
        private static async System.Threading.Tasks.Task<string> ReadContent(StreamReader contentStreamReader)
        {
            StringBuilder result = new StringBuilder();
            int contentChar;
            do
            {
                if (IfTargetChar('['))
                {
                    while (contentChar != -1 && !IfTargetChar(']'))
                    {
                    }
                    if (IfTargetChar('('))
                    {
                        char[] buffer = new char[AppPathInfo.FilePath.Length];
                        await contentStreamReader.ReadAsync(buffer).ConfigureAwait(false);
                        if (new string(buffer) == AppPathInfo.FilePath)
                        {
                            result.Append(AppPathInfo.ContentPath);
                        }
                        result.Append(buffer);
                    }
                }
            } while (contentChar != -1);
            return result.ToString();
            bool IfTargetChar(char c)
            {
                contentChar = contentStreamReader.Read();
                if (contentChar != -1)
                {
                    result.Append((char)contentChar);
                }
                return (char)contentChar == c;
            }
        }
        private DateTime GetModificationDate(IFileInfo info, DateTime defaultDate) {
            return _fileSystem.File.Exists(GetOutputPath(info.Name))?info.LastWriteTime:defaultDate;
        }
        protected override string GetOutputPath(string fileName)
        {
            return Path.Combine(AppPathInfo.OutputPath, FilePath, Path.GetFileNameWithoutExtension(fileName)+OutputType);
        }
    }
}
