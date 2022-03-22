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
        public static bool openEmulator = true;

        static void Main(string[] args)
        {
            BuildROM();
            if (openEmulator)
            {
                LaunchEmulator();
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        private static void BuildROM()
        {
            string args = $"compile \"{proj_path}\" \"{input_rom}\" \"{output_rom}\"";
            openEmulator = true;

            using (Process p = new Process())
            {
                p.StartInfo.FileName = coilsnake_cli;
                p.StartInfo.Arguments = args;
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
                        openEmulator = false;
                }
            }
            catch { }
        }

        private static void LaunchEmulator()
        {
            string args = $"-L \"D:\\Games\\Retroarch\\cores\\snes9x_libretro.dll\" \"{output_rom}\"";

            Process p = new Process();
            p.StartInfo.FileName = emulator;
            p.StartInfo.Arguments = args;
            p.Start();

            Console.WriteLine("\nGame launched!");
        }
    }
}
