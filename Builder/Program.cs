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
        public static string coilsnake_cli = @"C:\Python38\Scripts\coilsnake-cli.exe";
        public static string proj_path = @"C:/Users/ryans/Documents/GitHub/EarthBound-Mod-Menu";
        public static string input_rom = @"D:/Games/Retroarch/ROMS/SNES/Mods/EarthBound/EarthBound (Expanded).sfc";
        public static string output_rom = @"D:/Games/Retroarch/ROMS/SNES/Mods/EarthBound/EarthBound (Mod).smc";
        public static string emulator = @"D:\Games\Retroarch\Retroarch.exe";

        static void Main(string[] args)
        {
            while(true)
            {
                CompileAndLaunch();
                Console.WriteLine("\nGame launched! Press ENTER to compile again.\n\n");
                while(!Console.ReadKey(true).Key.Equals(ConsoleKey.Enter)) { }
            }
        }

        static void CompileAndLaunch()
        {
            KillCMD();
            Build();
            Launch();
        }

        private static void KillCMD()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("coilsnake-cli"))
                    proc.Kill();
            }
            catch { }
        }

        private static void Build()
        {
            string args = $"compile \"{proj_path}\" \"{input_rom}\" \"{output_rom}\"";

            using (Process p = new Process())
            {
                p.StartInfo.FileName = coilsnake_cli;
                p.StartInfo.Arguments = args;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                // Set event handler
                p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                // Start the process
                p.Start();
                // Start the asynchronous read
                p.BeginOutputReadLine();
                p.WaitForExit();
            }
        }

        static void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private static void Launch()
        {
            string args = $"-L \"D:\\Games\\Retroarch\\cores\\snes9x_libretro.dll\" \"{output_rom}\"";

            Process p = new Process();
            p.StartInfo.FileName = emulator;
            p.StartInfo.Arguments = args;
            p.Start();
        }
    }
}
