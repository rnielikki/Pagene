namespace Pagene.Editor
{
    internal class FileTitlePair
    {
        //Works only in the list with public
        public string FilePath { get; private set; }
        public string Title { get; set; }
        internal FileTitlePair(string path, string title)
        {
            FilePath = path;
            Title = title;
        }
    }
}
