namespace Pagene.Editor
{
    internal class FileTitlePair
    {
        //Works only in the list with public
        public string FileName { get; }
        public string Title { get; set; }
        internal FileTitlePair(string filePath, string title)
        {
            FileName = filePath;
            Title = title;
        }
        public override bool Equals(object obj)
        {
            var pair = obj as FileTitlePair;
            return pair switch
            {
                null => false,
                _ => FileName.Equals(pair.FileName)
            };
        }
        public override int GetHashCode() => FileName.GetHashCode();
    }
}
