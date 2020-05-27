using System;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class CodeWindow : Form
    {
        internal string Language { get; private set; }
        internal bool OK { get; private set; }
        public CodeWindow()
        {
            InitializeComponent();
        }
        protected void OKButton_Click(object sender, EventArgs e)
        {
            Language = LanguageBox.Text;
            OK = true;
            Close();
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
