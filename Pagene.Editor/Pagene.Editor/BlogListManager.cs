using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class BlogListManager : Form
    {
        private readonly Converter.Converter _converter;
        private readonly BlogPostLoader _loader = new BlogPostLoader();
        private IEnumerable<string> _tags;
        public BlogListManager(Converter.Converter converter)
        {
            _converter = converter;
            //TODO: lock directory and files
            InitializeComponent();
            BlogListUI.DisplayMember = nameof(FileTitlePair.Title);
            BlogListUI.ValueMember = nameof(FileTitlePair.FilePath);
            BlogListUI.DataSource = _loader.LoadPosts();
            _tags = _loader.GetTags();
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            string item = BlogListUI.SelectedValue.ToString();
            using var editForm = new EditWindow(await _loader.GetBlogItem(item).ConfigureAwait(true), item, _tags);
            editForm.FormClosed += Test;
            editForm.ShowDialog(this);
        }

        private async void Test(object sender, EventArgs e)
        {
            var targetSource = (sender as EditWindow);
            if (targetSource.Saved)
            {
                await _loader.SaveBlogItem(targetSource.Item, targetSource.FileName).ConfigureAwait(true);
            }
        }
        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to convert?", "Converting posts", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ConvertButton.Enabled = false;
                await _converter.Convert().ConfigureAwait(true);
                ConvertButton.Enabled = true;
                _tags = _loader.GetTags();
                MessageBox.Show("Done.");
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            using var editForm = new EditWindow(_loader.GetTags());
            editForm.ShowDialog(this);
        }
    }
}
