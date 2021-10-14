using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using SmartMacros.DrawingsMacros;

namespace SmartMacros.DrawingsMacros
{
    public partial class CloneGridsForm : Form
    {
        List<Grid> MainGrids;

        public CloneGridsForm()
        {
            TopMost = true;
            InitializeComponent();
            button2.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            MainGrids = CloneGrids.PickGrids();

            if (MainGrids.Count > 0)
                button2.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var secondaryGrid = CloneGrids.PickGrids();

            if(secondaryGrid != null)
            CloneGrids.CloneParamGrids(MainGrids, secondaryGrid);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new FormsClass().GoToWebSite(); 
        }
    }
}
