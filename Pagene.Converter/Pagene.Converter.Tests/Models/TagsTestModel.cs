using System.Collections.Generic;

namespace Pagene.Converter.Tests.Models
{
    internal static class TagsTestModel
    {
        internal static readonly IEnumerable<string> tags1Duplicated = new string[] { "chocolate", "chocolate", "ice cream", "milk" };
        internal static readonly IEnumerable<string> tags1 = new string[] { "chocolate", "ice cream", "milk" };
        internal static readonly IEnumerable<string> tags2 = new string[] { "bread", "cheese", "chocolate", "ice cream", "juice" };
        internal static readonly IEnumerable<string> intersection = new string[] { "bread", "cheese", "chocolate", "ice cream", "milk", "juice" };
    }
}
