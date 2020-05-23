using System;
using System.IO;
using System.IO.Abstractions;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class Starter : Form
    {
        private readonly Converter.Converter _converter;
        private readonly LinkWatcher _linkWatcher;
        public Starter(Converter.Converter converter)
        {
            _converter = converter;
            InitializeComponent();
            _linkWatcher = new LinkWatcher(converter, new FileSystemEventHandler(LoadFromChange), new RenamedEventHandler(LoadFromRename));
        }
        private void LoadFromChange(object sender, FileSystemEventArgs e)
        {
            Invoke((MethodInvoker)delegate { Hide(); });
            Application.Run(new BlogListManager(_converter));
            _linkWatcher.RemoveEvents();
        }
        private void LoadFromRename(object sender, FileSystemEventArgs e)
        {
            if (e.Name == "files.lnk")
            {
                LoadFromChange(sender, e);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
