using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class EditWindow
    {
        // This one has bug with rendering, this should be fixed with WinForms layout feature.
        private readonly Dictionary<string, Label> _tagPairs = new Dictionary<string, Label>(StringComparer.OrdinalIgnoreCase);
        private readonly List<Label> _tagList = new List<Label>();
        private void AddTag(string tag)
        {
            if (!_tagPairs.ContainsKey(tag))
            {
                var label = new Label();
                Controls.Add(label);
                SetupLabel(label);
                RegisterTag(tag, label);
            }
            void SetupLabel(Label label)
            {
                label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                label.Text = tag;
                label.Top = TagBox.Top;
                label.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                label.AutoSize = true;
                label.BackColor = System.Drawing.Color.Gold;
                label.ForeColor = System.Drawing.Color.Black;
                label.Left = ((_tagPairs.Count == 0) ? 10 : _tagList.Last().Right)+10;
                label.Click += RemoveTag;
            }
        }
        private void AddTagRange(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                AddTag(tag);
            }
        }
        private void RemoveTag(object sender, EventArgs e)
        {
            Label label = (sender as Label);
            string labelText = (sender as Label)?.Text;
            UnregisterTag(labelText, label);
        }
        private void RegisterTag(string tag, Label label)
        {
            _tagPairs.Add(tag, label);
            _tagList.Add(label);
        }
        private void UnregisterTag(string tag, Label label)
        {
            var moveLength = label.Width;
            var index = _tagList.IndexOf(label);
            _tagPairs.Remove(tag);
            _tagList.Remove(label);
            Controls.Remove(label);
            foreach (Label target in _tagList.TakeLast(_tagList.Count - index))
            {
                target.Left = target.Left-moveLength-10;
            }
        }
    }
}
