using Pagene.BlogSettings;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Pagene.Converter
{
    internal class Cleaner
    {
        private readonly IFileSystem _fileSystem;
        internal Cleaner(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
       internal void Clean(string path, IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                _fileSystem.File.Delete(System.IO.Path.Combine(path, fileName));
                _fileSystem.File.Delete(System.IO.Path.Combine(AppPathInfo.HashPath, path, fileName+".hashfile"));
            }
        }
        internal void CleanTags(TagManager tagManager)
        {
            var cleanTargetTags = tagManager.GetRemovedTags();
            if (!cleanTargetTags.Any()) return;
            foreach (var targetTag in cleanTargetTags)
            {
                _fileSystem.File.Delete($"{AppPathInfo.BlogTagPath}{targetTag.ToLower()}.json");
            }
        }
    }
}
