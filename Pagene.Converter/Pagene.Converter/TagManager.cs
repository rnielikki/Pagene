using Pagene.Models;
using System.Collections.Generic;

namespace Pagene.Converter
{
    static class TagManager
    {
        private readonly static Dictionary<string, BlogEntry[]> _tagMap = new Dictionary<string, BlogEntry[]>();
        internal static void AddTag(IEnumerable<string> tags, BlogEntry entry)
        {
            foreach(string tag in tags)
            {
            }
        }
        /*
        void Serialize()
        {
        }
        */
    }
}
