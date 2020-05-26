using System;
using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class LinkWindow : Form
    {
        internal string Link { get; private set; }
        internal string Title { get; private set; }
        internal bool OK { get; private set; }
        public LinkWindow()
        {
            InitializeComponent();
        }

        private void LinkWindow_Load(object sender, EventArgs e)
        {
            LinkTarget.Text = "https://";
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Link = LinkTarget.Text;
            Title = TitleTarget.Text;
            OK = true;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
