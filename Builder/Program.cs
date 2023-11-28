using Shell32;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EBModMenu
{
    class Program
    {
        // How to set up builder
        // 1. Install Python 3.9: https://www.python.org/downloads/release/python-392/
        // 2. Install Microsoft Visual C++ 14.0 for Visual Studio: https://visualstudio.microsoft.com/downloads/
        // 3. Git clone Coilsnake repository: https://github.com/pk-hack/CoilSnake
        // 4. Add path similar to the following to your system environment PATH variable:
        //      C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x86
        // 5. In command prompt, run:
        //      python -m pip install pip==18.1
        // 6. cd into Coilsnake repo and run:
        //      python setup.py develop
        // 7. If successful, coilsnake-cli.exe should appear in:
        //      C:\Python\Scripts
        // 8. Replace the .lnk files with shortcuts to your Expanded 6 MB ROM, coilsnake-cli.exe & emulator
        // 9. Run ModMenuBuilder.exe to generate new output ROM for testing.
        // More info: https://github.com/pk-hack/CoilSnake/blob/master/DEVELOPMENT.md

        [STAThread]
        static void Main(string[] args)
        {
            BuildROM();
            if (openEmulatorWhenFinished)
            {
                LaunchEmulator();
            }

            if (!openEmulatorWhenFinished)
            {
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
            }

        }

        public static string proj_path = @"..\..\..\CoilSnakeProj";
        public static string coilsnake_cli = @".\Dependencies\CoilSnakeCLI\coilsnake-cli.lnk";
        public static string input_rom = @".\Dependencies\Rom\Rom.lnk";
        public static string emulator_lnk = @".\Dependencies\Emulator\Emulator.lnk";
        public static string output_rom = @"..\..\..\Output\EarthBound_Mod.smc";
        public static bool openEmulatorWhenFinished = true;

        private static void BuildROM()
        {
            string coilsnakeCLIPath = GetAbsolutePath(GetShortcutTargetFile(coilsnake_cli));

            string args = $"compile \"{GetAbsolutePath(proj_path)}\" " +
                $"\"{GetAbsolutePath(GetShortcutTargetFile(input_rom))}\" \"{GetAbsolutePath(output_rom)}\"";
            openEmulatorWhenFinished = true;

            using (Process p = new Process())
            {
                p.StartInfo.FileName = coilsnakeCLIPath;
                p.StartInfo.Arguments = args;
                Console.WriteLine($"{coilsnakeCLIPath} {args}");
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                // Set event handler
                p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                // Start the process
                p.EnableRaisingEvents = true;
                p.Exited += ProcessEnded;
                p.Start();
                // Start the asynchronous read
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.Close();
                p.Dispose();
            }
        }

        static void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        static void ProcessEnded(object sender, EventArgs e)
        {
            var process = sender as Process;
            try
            {
                using (StreamReader sr = process.StandardError)
                {
                    string error = sr.ReadToEnd();
                    Console.WriteLine(error);
                    if (error != "")
                        openEmulatorWhenFinished = false;
                }
            }
            catch { }
        }

        private static void LaunchEmulator()
        {
            //string args = $"-L \"D:\\Games\\Retroarch\\cores\\snes9x_libretro.dll\" \"{output_rom}\"";
            string args = $"\"{GetAbsolutePath(output_rom)}\"";

            Process p = new Process();
            p.StartInfo.FileName = emulator_lnk;
            p.StartInfo.Arguments = args;
            p.Start();

            Console.WriteLine("\nGame launched!");
        }

        public static string GetShortcutTargetFile(string shortcutPath)
        {
            Shell shell = new Shell();
            Folder folder = shell.NameSpace(GetAbsolutePath(Path.GetDirectoryName(shortcutPath)));
            FolderItem folderItem = folder.ParseName(Path.GetFileName(shortcutPath));
            if (folderItem != null)
            {
                ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }
            else
                Console.WriteLine($"Error: Failed to get target from .lnk: {shortcutPath}");

            return shortcutPath;
        }

        public static string GetAbsolutePath(string path)
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), path));
        }
    }
}
