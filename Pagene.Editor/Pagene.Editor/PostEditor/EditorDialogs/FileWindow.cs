using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Pagene.BlogSettings;

namespace Pagene.Editor
{
    internal partial class FileWindow : Form
    {
        private readonly string fullPath;
        private readonly int _thumbWidth;
        private readonly int _thumbHeight;
        private readonly float _thumbRatio;
        internal string FileName { get; private set; }
        internal bool OK { get; private set; }
        public FileWindow()
        {
            InitializeComponent();
            ErrorMessage.Hide();
            InitializeList();
            fullPath = Path.GetFullPath(AppPathInfo.BlogFilePath);
            _thumbWidth = PreviewPicture.Width;
            _thumbHeight = PreviewPicture.Height;
            _thumbRatio = (float)_thumbWidth / _thumbHeight;
        }
        private void InitializeList()
        {
            var files = new DirectoryInfo(AppPathInfo.BlogFilePath)
                .GetFiles("*", SearchOption.TopDirectoryOnly)
                .Where(IsImageFile)
                .OrderByDescending(file => file.CreationTime);
            var imageList = new ImageList
            {
                ImageSize = new Size(48, 48)
            };
            FileList.LargeImageList = imageList;
            foreach (var file in files)
            {
                var item = new ListViewItem(file.Name);
                var image = Icon.ExtractAssociatedIcon(file.FullName);
                FileList.LargeImageList.Images.Add(file.Name,image);
                item.ImageKey = file.Name;

                FileList.Items.Add(item);
            }
            FileList.SelectedIndexChanged += Preview;
        }
        private void OpenPathButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", fullPath);
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
           FileName = FileList.FocusedItem.Text;
           OK = true;
           Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
