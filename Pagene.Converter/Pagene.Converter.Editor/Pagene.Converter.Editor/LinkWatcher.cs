using System.IO;
using System.Windows.Forms;

namespace Pagene.Converter.Editor
{
    class LinkWatcher
    {
        FileSystemWatcher _watcher;
        FileSystemEventHandler _e;
        RenamedEventHandler _r;
        internal LinkWatcher(Converter converter, FileSystemEventHandler e, RenamedEventHandler r)
        {
            converter.Initialize();
            _watcher = new FileSystemWatcher("inputs/contents", "files.lnk");
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _e = e;
            _r = r;
            _watcher.Created += e;
            _watcher.Renamed += r;
            _watcher.EnableRaisingEvents = true;
        }
        internal void RemoveEvents()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= _e;
            _watcher.Renamed -= _r;
            _watcher.Dispose();
        }
    }
}
