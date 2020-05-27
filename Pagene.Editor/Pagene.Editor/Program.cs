using System;
using System.Windows.Forms;

namespace Pagene.Editor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new BlogListManager(new Converter.Converter()));
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"The operation is not completed." +
                    $"{Environment.NewLine}Check if the blog format is corrupted." +
                    $"{Environment.NewLine}You may need to edit file manually." +
                    $"{Environment.NewLine}File : {ex.Message}");
                Application.Exit();
            }
        }
    }
}
