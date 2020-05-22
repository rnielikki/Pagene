using System;
using System.Windows.Forms;

namespace Pagene.Converter.Editor
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TODO: lock directory and files

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (System.IO.File.Exists("inputs/contents/files.lnk"))
            {
                Application.Run(new BlogPostList(new Converter()));
            }
            else
            {
                Application.Run(new Starter(new Converter()));
            }
        }
    }
}
