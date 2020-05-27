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
        private void SyntaxImage_Click(object sender, EventArgs e)
        {
            using FileWindow fileWindow = new FileWindow();
            fileWindow.FormClosed += AddImage;
            fileWindow.ShowDialog(this);
        }
        private void SyntaxCode_Click(object sender, EventArgs e)
        {
            using CodeWindow codeWindow = new CodeWindow();
            codeWindow.FormClosed += AddCodeLanguage;
            codeWindow.ShowDialog(this);
        }
        private void SyntaxLink_Click(object sender, EventArgs e)
        {
            using var linkForm = new LinkWindow();
            linkForm.FormClosed += AddLink;
            linkForm.ShowDialog(this);
        }
        //---
        //should remove reputation for checking.......
        //make some abstraction, but >> **don't ruin the form edit window** <<
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
        private void AddImage(object sender, EventArgs e)
        {
            FileWindow senderWindow = (sender as FileWindow);
            if (!senderWindow.OK)
            {
                return;
            }
            _markdown.Image($"files/{senderWindow.FileName}");
        }
        private void AddCodeLanguage(object sender, EventArgs e)
        {
            CodeWindow senderWindow = (sender as CodeWindow);
            if (!senderWindow.OK)
            {
                return;
            }
            _markdown.CodeLines(senderWindow?.Language);
        }
    }
}
