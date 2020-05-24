using Pagene.Models;
using Pagene.Reader.PostSerializer;
using System;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class BlogListManager : Form
    {
        private readonly Converter.Converter _converter;
        private readonly BlogPostLoader _loader = new BlogPostLoader();
        public BlogListManager(Converter.Converter converter)
        {
            _converter = converter;
            //TODO: lock directory and files
            InitializeComponent();
            BlogListUI.DisplayMember = nameof(FileTitlePair.Title);
            BlogListUI.ValueMember = nameof(FileTitlePair.FilePath);
            BlogListUI.DataSource = _loader.LoadPosts();
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            string item = BlogListUI.SelectedValue.ToString();
            using var editForm = new EditWindow(await _loader.GetBlogItem(item).ConfigureAwait(true));
            editForm.ShowDialog(this);
        }

        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to convert?", "Converting posts", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ConvertButton.Enabled = false;
                await _converter.Convert().ConfigureAwait(true);
                ConvertButton.Enabled = true;
                MessageBox.Show("Done.");
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            using var editForm = new EditWindow();
            editForm.ShowDialog(this);
        }
    }
}
