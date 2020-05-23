using System.Windows.Forms;

namespace Pagene.Editor
{
    public partial class BlogListManager : Form
    {
        private readonly Converter.Converter _converter;
        public BlogListManager(Converter.Converter converter)
        {
            _converter = converter;
            //TODO: lock directory and files
            InitializeComponent();
            BlogListUI.DisplayMember = nameof(FileTitlePair.Title);
            BlogListUI.ValueMember = nameof(FileTitlePair.FilePath);
            BlogListUI.DataSource = new BlogPostLoader().LoadPosts();
        }

        private void EditButton_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(BlogListUI.SelectedValue.ToString());
        }

        private async void ConvertButton_Click(object sender, System.EventArgs e)
        {
            ConvertButton.Enabled = false;
            await _converter.Convert();
            ConvertButton.Enabled = true;
        }
    }
}
