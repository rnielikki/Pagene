﻿using System;
using System.Windows.Forms;
using Markdig;

namespace Pagene.Editor
{
    public partial class PreviewWindow : Form
    {
        public PreviewWindow(Models.BlogItem item)
        {
            InitializeComponent();
            MessageBox.Show($"Sorry, Now we have technical isseus, so we can't render it right now.{Environment.NewLine}Instead, we show HTML codes here.");
            TitleBox.Text = item.Title;
            _ = SetPreview(Markdown.ToHtml(item.Content));
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private async System.Threading.Tasks.Task SetPreview(string content) {
            await PreviewContent.EnsureCoreWebView2Async(null).ConfigureAwait(true);
           PreviewContent.NavigateToString(content);
        }
    }
}
