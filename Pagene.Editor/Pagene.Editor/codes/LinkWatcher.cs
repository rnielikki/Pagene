using System.IO;

namespace Pagene.Editor
{
    class LinkWatcher
    {
        private readonly FileSystemWatcher _watcher;
        internal LinkWatcher(Converter.Converter converter, FileSystemEventHandler e, RenamedEventHandler r)
        {
            converter.Initialize();
            _watcher = new FileSystemWatcher("inputs/contents", "files.lnk")
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            _watcher.Created += e;
            _watcher.Renamed += r;
            _watcher.EnableRaisingEvents = true;
        }
        internal void RemoveEvents()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose(); // this should release all event subscriptions
        }
    }
}
