using System;
using System.IO;
using System.Windows.Forms;

namespace Pagene.Converter.Editor
{
    public partial class Starter : Form
    {
        private Converter _converter;
        private LinkWatcher _linkWatcher;
        public Starter(Converter converter)
        {
            _converter = converter;
            InitializeComponent();
            _linkWatcher = new LinkWatcher(converter, new FileSystemEventHandler(_load), new RenamedEventHandler(_loadFromRename));
        }
        private void _load(object sender, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate { Hide(); });
            Application.Run(new BlogPostList(_converter));
            _linkWatcher.RemoveEvents();
        }
        private void _loadFromRename(object sender, FileSystemEventArgs e)
        {
            if (e.Name == "files.lnk")
            {
                _load(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
