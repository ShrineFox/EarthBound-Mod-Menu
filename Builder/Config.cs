using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBModMenu
{
    public class Config
    {
#if DEBUG
        public string CoilsnakeCLIPath { get; set; } = @"..\..\..\venv\Scripts";
        public string InputROMPath { get; set; } = @"..\..\..\EarthBound.smc";
        public string OutputROMPath { get; set; } = @".\Output\EarthBound_Mod_Menu.smc";
        public string EmulatorPath { get; set; } = @"..\..\..\venv\Snes9x\snes9x-x64.exe";
#endif
#if !DEBUG
        public string CoilsnakeCLIPath { get; set; } = @"C:\Python\Scripts\coilsnake-cli.exe";
        public string InputROMPath { get; set; } = @".\Dependencies\InputROM\EarthBound.smc";
        public string OutputROMPath { get; set; } = @".\Output\EarthBound_Mod_Menu.smc";
        public string EmulatorPath { get; set; } = @".\Dependencies\Emulator\snes9x-x64.exe";
#endif
        public bool LaunchEmuAfterBuild { get; set; } = true;

        public void SaveJson(Config settings)
        {
            File.WriteAllText("./Config.json", JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented));
        }

        public Config LoadJson()
        {
            if (!File.Exists("./Config.json"))
                return new Config();

            string jsonText = File.ReadAllText(Path.GetFullPath("./Config.json"));
            Config config = JsonConvert.DeserializeObject<Config>(jsonText);
            return config;
        }
    }
}