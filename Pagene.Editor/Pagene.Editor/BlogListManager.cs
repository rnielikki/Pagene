using Pagene.BlogSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class BlogListManager : Form
    {
        private readonly Converter.Converter _converter;
        private readonly BlogPostLoader _loader = new BlogPostLoader();
        private IEnumerable<string> _tags;
        private readonly BindingList<FileTitlePair> data;
        public BlogListManager(Converter.Converter converter)
        {
            _converter = converter;
            _converter.Initialize();
            data = new BindingList<FileTitlePair>(_loader.LoadPosts());
            //TODO: lock directory and files
            InitializeComponent();
            BlogListUI.DisplayMember = nameof(FileTitlePair.Title);
            BlogListUI.ValueMember = nameof(FileTitlePair.FileName);
            BlogListUI.DataSource = data;
            _tags = _loader.GetTags();
        }

        private async void EditButton_Click(object sender, EventArgs e)
        {
            if (BlogListUI.SelectedValue == null)
            {
                MessageBox.Show("Nothing is selected");
                return;
            }
            string item = BlogListUI.SelectedValue.ToString();
            using var editForm = new EditWindow(await _loader.GetBlogItem(item).ConfigureAwait(true), item, _tags);
            editForm.FormClosed += Save;
            editForm.ShowDialog(this);
        }

        private async void Save(object sender, EventArgs e)
        {
            var targetSource = (sender as EditWindow);
            var newTitle = targetSource.Item.Title;
            var fileName = targetSource.FileName ?? _loader.GetNameFromTitle(newTitle);
            if (targetSource.Saved)
            {
                await _loader.SaveBlogItem(targetSource.Item, fileName).ConfigureAwait(true);
                var newPair = new FileTitlePair(fileName, newTitle);
                if (targetSource.EditType == PostType.NewPost)
                {
                    data.Insert(0, newPair);
                    BlogListUI.SelectedIndex = 0;
                }
                else
                {
                    int index = data.IndexOf(newPair);
                    data.RemoveAt(index);
                    data.Insert(index, newPair);
                    BlogListUI.SelectedIndex = index;
                }
            }
        }
        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to convert?", "Converting posts", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                ConvertButton.Enabled = false;
                await _converter.BuildAsync().ConfigureAwait(true);
                ConvertButton.Enabled = true;
                _tags = _loader.GetTags();
                MessageBox.Show("Done.");
            }
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            using var editForm = new EditWindow(_loader.GetTags());
            editForm.FormClosed += Save;
            editForm.ShowDialog(this);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (BlogListUI.SelectedValue == null)
            {
                MessageBox.Show("Nothing is selected");
                return;
            }
            var selectedItem = BlogListUI.SelectedItem as FileTitlePair;
            string fileName = selectedItem.FileName;
            var result = MessageBox.Show($"Are you sure to remove? :{Environment.NewLine}{selectedItem.Title}{Environment.NewLine}({fileName})", "Delete a post", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                data.Remove(selectedItem);
                _loader.RemovePost(fileName);
            }
        }
    }
}
