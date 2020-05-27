using System;
using System.Windows.Forms;

namespace Pagene.Editor.Markdowns
{
    public class MarkdownAdder
    {
        private readonly RichTextBox _textBox;
        public MarkdownAdder(RichTextBox textBox)
        {
            _textBox = textBox;
        }
        public void Header(int level)
        {
            if (level < 1)
            {
                throw new ArgumentException("Header level cannot be less than 1.");
            }
            InsertFirstLine($"{new string('#', level)} ");
        }
        public void Emphasize()
        {
            _textBox.SelectedText = $"*{GetSelectedOrDefault("Emphasized Text")}*";
        }
        public void Bold()
        {
            _textBox.SelectedText = $"**{GetSelectedOrDefault("Bold")}**";
        }
        public void Link(string link, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                title = $" \"{title.Replace("\"", "")}\"";
            }
            _textBox.SelectedText = $"[{GetSelectedOrDefault("Link")}]({link}{title})";
        }
        public void Horizon()
        {
            InsertFirstLine($"---{Environment.NewLine}");
        }
        public void Quote()
        {
            InsertFirstLine("> ");
        }
        public void Code()
        {
            _textBox.SelectedText = $"`{GetSelectedOrDefault("code")}`";
        }
        public void Image(string fileName)
        {
            _textBox.SelectedText = $"![{GetSelectedOrDefault("Image Alt")}]({fileName})";
        }
        public void CodeLines(string language)
        {
            _textBox.SelectedText = $"{Environment.NewLine}```{language}{Environment.NewLine}{GetSelectedOrDefault("//any code here")}{Environment.NewLine}```{Environment.NewLine}";
        }
        //privates
        private void InsertFirstLine(string s)
        {
            int startIndex = _textBox.GetFirstCharIndexOfCurrentLine();
            _textBox.Select(startIndex, 0);
            _textBox.SelectedText = s;
        }
        private string GetSelectedOrDefault(string placeholder)
        {
            string currentText = _textBox.SelectedText;
            return string.IsNullOrEmpty(currentText) ? placeholder : currentText;
        }
    }
}
