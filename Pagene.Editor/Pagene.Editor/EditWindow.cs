using Pagene.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class EditWindow : Form
    {
        public EditWindow(BlogItem item)
        {
            InitializeComponent();
            TitleBox.Text = item.Title;
            ContentBox.Text = item.Content;
            AddTagRange(item.Tags);

            IEnumerable<string> test = new string[] { "test1", "aaa", "choco", "milk" };
            TagBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TagBox.AutoCompleteCustomSource.AddRange(test.ToArray());
        }
        public EditWindow() : this(new BlogItem
        {
            Title = "",
            Content = "",
            Tags = Enumerable.Empty<string>()
        }
        )
        { } // create new

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var item = new BlogItem { Title = TitleBox.Text, Content = ContentBox.Text, Tags = _tagPairs.Keys };
            //send this back.
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
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
    }
}
