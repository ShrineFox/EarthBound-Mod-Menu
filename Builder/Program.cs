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
using ShrineFox.IO;

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
            VerifyPaths();
#if DEBUG
            BuildROM();
#endif
            if (config.LaunchEmuAfterBuild)
            {
                LaunchEmulator();
            }
            else
            {
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
            }

        }

        private static void VerifyPaths()
        {
            if (!File.Exists("./Config.json"))
                config.SaveJson(config);
            else
                config = config.LoadJson();

            if (GetPathFromShortcut(config.CoilsnakeCLIPath) == "")
            {
                string coilsnakeCLIPath = "";
                while (!File.Exists(coilsnakeCLIPath))
                {
                    var selectedFiles = WinFormsDialogs.SelectFile("Choose coilsnake_cli.exe", false, new string[] { "Executable (.exe)" });
                    if (selectedFiles.Count > 0)
                        coilsnakeCLIPath = selectedFiles.First();
                }
                config.CoilsnakeCLIPath = coilsnakeCLIPath;
                config.SaveJson(config);
            }

            if (GetPathFromShortcut(config.InputROMPath) == "")
            {
                string inputROM = "";
                while (!File.Exists(inputROM))
                {
                    var selectedFiles = WinFormsDialogs.SelectFile("Choose Original EB ROM", false, new string[] { "Super Nintendo ROM (.smc)" });
                    if (selectedFiles.Count > 0)
                        inputROM = selectedFiles.First();
                }
                config.InputROMPath = inputROM;
                config.SaveJson(config);
            }

            if (GetPathFromShortcut(config.OutputROMPath) == "")
            {
                string outROMDir = "";
                while (!Directory.Exists(outROMDir))
                {
                    outROMDir = WinFormsDialogs.SelectFolder("Choose Destination for Modded ROM");
                }
                config.OutputROMPath = Path.Combine(outROMDir, "EarthBound_Mod_Menu.smc");
                config.SaveJson(config);
            }

            if (GetPathFromShortcut(config.EmulatorPath) == "")
            {
                string emuPath = "";
                while (!File.Exists(emuPath))
                {
                    var selectedFiles = WinFormsDialogs.SelectFile("Choose Emulator .exe", false, new string[] { "Executable (.exe)" });
                    if (selectedFiles.Count > 0)
                        emuPath = selectedFiles.First();
                }
                config.EmulatorPath = emuPath;
                config.SaveJson(config);
            }
        }

        private static string GetPathFromShortcut(string path)
        {
            if (path.EndsWith(".lnk"))
                path = GetAbsolutePath(GetShortcutTargetFile(config.InputROMPath));

            if (!Directory.Exists(path) && !File.Exists(path))
                return "";

            return path;
        }

        public static Config config = new Config();
        public static string proj_path = @"..\..\..\CoilSnakeProj";

        private static void BuildROM()
        {
            string coilsnakeCLIPath = GetAbsolutePath(config.CoilsnakeCLIPath);
            string inputROM = GetAbsolutePath(config.InputROMPath);

            // If ROM is 3MB, expand to 6MB and use as input ROM
            if (new FileInfo(inputROM).Length == 3145728)
            {
                inputROM = ExpandROM(coilsnakeCLIPath, inputROM);
                if (inputROM == "")
                {
                    Console.WriteLine("Failed to expand input ROM.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    config.InputROMPath = inputROM;
                    config.SaveJson(config);
                }
            }

            string args = $"compile \"{GetAbsolutePath(proj_path)}\" " +
                $"\"{inputROM}\" \"{GetAbsolutePath(config.OutputROMPath)}\"";

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

        private static string ExpandROM(string coilsnakeCLIPath, string inputROM)
        {
            string newPath = Path.Combine(Path.GetDirectoryName(inputROM), "EarthBound_Expanded.smc");
            File.Copy(inputROM, newPath, true);

            using (Process p = new Process())
            {
                p.StartInfo.FileName = coilsnakeCLIPath;
                p.StartInfo.Arguments = $"expand \"{newPath}\" true";
                Console.WriteLine($"{coilsnakeCLIPath} {p.StartInfo.Arguments}");
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

            if (File.Exists(newPath))
                return newPath;
            else
                return "";
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
                        config.LaunchEmuAfterBuild = false;
                }
            }
            catch { }
        }

        private static void LaunchEmulator()
        {
            //string args = $"-L \"D:\\Games\\Retroarch\\cores\\snes9x_libretro.dll\" \"{output_rom}\"";
            string args = $"\"{GetAbsolutePath(config.OutputROMPath)}\"";

            Process p = new Process();
            p.StartInfo.FileName = GetAbsolutePath(config.EmulatorPath);
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
