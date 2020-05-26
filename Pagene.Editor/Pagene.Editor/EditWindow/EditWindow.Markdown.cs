using System;

namespace Pagene.Editor
{
    public partial class EditWindow
    {
        private void SyntaxHeadline_Click(object sender, EventArgs e) => _markdown.Header(1);
        private void SyntaxHeadline2_Click(object sender, EventArgs e) => _markdown.Header(2);
        private void SyntaxBoldText_Click(object sender, EventArgs e) => _markdown.Bold();
        private void SyntaxItalic_Click(object sender, EventArgs e) => _markdown.Emphasize();
        private void SyntaxHorizon_Click(object sender, EventArgs e) => _markdown.Horizon();
        private void SyntaxQuote_Click(object sender, EventArgs e) => _markdown.Quote();
        private void SyntaxCodeInline_Click(object sender, EventArgs e) => _markdown.Code();
        private void SyntaxCode_Click(object sender, EventArgs e) => _markdown.CodeLines("csharp");
        private void SyntaxLink_Click(object sender, EventArgs e)
        {
            using var linkForm = new LinkWindow();
            linkForm.FormClosed += AddLink;
            linkForm.ShowDialog(this);
        }
        private void AddLink(object sender, EventArgs e)
        {
            LinkWindow senderWindow = (sender as LinkWindow);
            if (!senderWindow.OK)
            {
                return;
            }
            string link = senderWindow?.Link;
            if (string.IsNullOrEmpty(link))
            {
                link = "http://example.com";
            }
            _markdown.Link(link, senderWindow.Title);
        }
    }
}
