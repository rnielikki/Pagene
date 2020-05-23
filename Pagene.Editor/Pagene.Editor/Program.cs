using System;
using System.Windows.Forms;

namespace Pagene.Editor
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (System.IO.File.Exists("inputs/contents/files.lnk"))
            {
                Application.Run(new BlogListManager(new Converter.Converter()));
            }
            else
            {
                Application.Run(new Starter(new Converter.Converter()));
            }
        }
    }
}
