using ShrineFox.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosModeEditor
{
    public partial class ChaosModeEditor : Form
    {
        public string chaosModeLocation = "../../../CoilsnakeProj/ccscript/_scripts/mainmenu/chaosmode.ccs";
        public ChaosModeEditor()
        {
            InitializeComponent();

            if (!File.Exists(chaosModeLocation))
            {
                MessageBox.Show($"Could not find file: {chaosModeLocation}");
                Environment.Exit(0);
            }
            else
                GetCurrentValues();
        }

        private void GetCurrentValues()
        {
            var lines = File.ReadAllLines(chaosModeLocation);
            foreach (var control in ShrineFox.IO.WinForms.EnumerateControls(this)
                .Where(x => x.GetType() == typeof(NumericUpDown)))
            {
                var numUpDwn = (NumericUpDown)control;

                string name = numUpDwn.Name;
                string search = $"goto({name.Replace("num_", "")})";
                if (lines.Any(x => x.Contains(search)))
                {
                    Match match = Regex.Match(lines.First(x => x.Contains(search)), @"\((\d+)\)");
                    if (match.Success)
                        numUpDwn.Value = int.Parse(match.Groups[1].Value);
                }
                else
                {
                    numUpDwn.Enabled = false;
                }
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines(chaosModeLocation).ToList();
            lines.RemoveRange(14, 12);

            var controls = WinForms.EnumerateControls(this)
                .Where(x => x.GetType() == typeof(NumericUpDown) && x.Enabled == true);
            List<NumericUpDown> numUpDwnList = new List<NumericUpDown>();
            foreach (var control in controls)
            {
                var numUpDwn = (NumericUpDown)control;
                numUpDwnList.Add(numUpDwn);
            }

            foreach (NumericUpDown numUpDwn in numUpDwnList.OrderBy(x => x.Value))
            {
                string gotoName = numUpDwn.Name.Replace("num_", "");
                lines.Insert(14, $"\tload_registers_global if result_is_greaterthan_orequal({numUpDwn.Value}) {{ goto({gotoName}) }}");
            }

            File.WriteAllLines(chaosModeLocation, lines);
            MessageBox.Show("Successfully updated Chaos Mode values!" +
                "\nBuild the project with Coilsnake to see changes.");
        }
    }
}
