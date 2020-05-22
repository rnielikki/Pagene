using System;

namespace Pagene.Converter
{
    internal class ShortCutPal
    {
        private string _path;

        internal ShortCutPal(string targetPath)
        {
            _path = System.IO.Path.GetFullPath(targetPath);
        }

        internal void CreateShortcut(string original, string link)
        {
            Console.WriteLine("To enhance input preview etc. you can  optionally run this command for maknig shortcut:");
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    Console.WriteLine($"New-Item -ItemType SymbolicLink -Target \"{_path}\\{original}\" -Path -Target \"{_path}\\{link}\"");
                    Console.WriteLine($"If you are not administrator of PowerShell, you can make {link} that connects to {original} manually.");
                    return;
                case PlatformID.Unix:
                    Console.WriteLine($"ln -s {_path}/{original} {_path}/{link}");
                    return;
                default:
                    return;
            }
        }
    }
}