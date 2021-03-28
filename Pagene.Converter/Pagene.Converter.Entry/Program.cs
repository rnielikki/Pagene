using System;

namespace Pagene.Converter.Entry
{
    internal static class Program
    {
        private static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowCommandError();
                return;
            }
            var converter = new Converter();
            switch (args[0])
            {
                case "init":
                        converter.Initialize();
                    return;
                case "clean":
                    converter.Clean();
                    return;
                case "build":
                    await converter.BuildAsync().ConfigureAwait(false);
                    return;
                case "rebuild":
                    await converter.RebuildAsync().ConfigureAwait(false);
                    return;
                default:
                    ShowCommandError();
                    return;
            }
        }
        private static void ShowCommandError() => Console.WriteLine("Parameters usage: (init|clean|build|rebuild)");
    }
}
