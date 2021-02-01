using Newtonsoft.Json;
using SmartTeklaModel.PluginAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var button = new UserRun(new Model(), "testID");







































            string output = JsonConvert.SerializeObject(new UserRun(new Model(), "testID"));

        }
    }
}
