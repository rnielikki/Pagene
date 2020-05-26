using Pagene.Editor.Markdowns;
using Pagene.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public enum PostType { NewPost, EditPost };
    public partial class EditWindow : Form
    {
        internal BlogItem Item { get; }
        internal bool Saved { get; private set; }
        internal string FileName { get; }
        internal PostType EditType { get; }
        private readonly MarkdownAdder _markdown;
        public EditWindow(BlogItem item, string fileName, IEnumerable<string> tags)
        {
            Item = item;
            FileName = fileName;
            EditType = (fileName==null)?PostType.NewPost:PostType.EditPost;
            InitializeComponent();
            Text = (string.IsNullOrEmpty(fileName))?"New Post":$"Editing {fileName}";
            _markdown = new MarkdownAdder(ContentBox);
            TitleBox.Text = item.Title;
            ContentBox.Text = item.Content;
            AddTagRange(item.Tags);

            TagBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TagBox.AutoCompleteCustomSource.AddRange(tags.ToArray());
        }
        public EditWindow(IEnumerable<string> tags) : this(new BlogItem
        {
            Title = "",
            Content = "",
            Tags = Enumerable.Empty<string>()
        }, null, tags
        )
        { } // create new

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveBlogItem();
            Saved = true;
            Close();
        }
        private void SaveBlogItem()
        {
            Item.Title = TitleBox.Text;
            Item.Content = ContentBox.Text;
            Item.Tags = _tagPairs.Keys;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!Saved)
            {
                var confirm = MessageBox.Show("Do you want to cancel without saving?", "Cancel", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    base.OnFormClosing(e);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                base.OnFormClosing(e);
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void AddTagButton_Click(object sender, EventArgs e)
        {
            string tag = TagBox.Text;
            if (!string.IsNullOrEmpty(tag))
            {
                AddTag(tag);
                TagBox.Text = "";
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            SaveBlogItem();
            using var linkForm = new PreviewWindow(Item);
            linkForm.ShowDialog(this);
        }
    }
}
