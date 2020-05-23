using System;

namespace Pagene.Converter.Entry
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowCommandError();
                return;
            }
            string path = "";
            if (args.Length > 1)
            {
                path = args[1];
            }
            var converter = new Converter(path);
            switch (args[0])
            {
                case "init":
                        converter.Initialize();
                    return;
                case "convert":
                    await converter.Convert();
                    return;
                default:
                    ShowCommandError();
                    return;
            }
        }
        private static void ShowCommandError() => Console.WriteLine("Parameters usage: (init|convert) (path:Optional)");
    }
}
