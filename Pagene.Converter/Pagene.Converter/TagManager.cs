using Pagene.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Utf8Json;

namespace Pagene.Converter
{
    class TagManager:IDisposable
    {
        //sometimes can be replaced to ConcurrentDictionary
        private readonly Dictionary<string, List<BlogEntry>> _tagMap = new Dictionary<string, List<BlogEntry>>();
        private readonly Stream fileStream;
        internal TagManager(System.IO.Abstractions.IFileSystem fileSystem)
        {
            fileStream = fileSystem.File.Open("meta.tags.json", FileMode.OpenOrCreate);
            if (fileStream.Length != 0)
            {
                _tagMap = JsonSerializer.Deserialize<Dictionary<string, List<BlogEntry>>>(fileStream);
            }
        }
        internal void AddTag(IEnumerable<string> tags, BlogEntry entry)
        {
            foreach(string tag in tags)
            {
                if (_tagMap.TryGetValue(tag, out List<BlogEntry> entries))
                {
                    entries.Add(entry);
                }
                else
                {
                    _tagMap.Add(tag, new List<BlogEntry>() { entry });
                }
            }
        }
        internal async System.Threading.Tasks.Task Serialize()
        {
            StreamWriter writer = new StreamWriter(fileStream);
            fileStream.Position = 0;
            await writer.WriteAsync(JsonSerializer.ToJsonString(_tagMap));
            writer.Flush();
        }
        public void Dispose()
        {
            fileStream.Close();
        }
    }
}
