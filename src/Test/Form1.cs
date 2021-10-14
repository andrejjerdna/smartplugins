using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model.Operations;

namespace Test
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> objctsInDrws = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var maсroBuilder = new MacroBuilder();
            maсroBuilder.Callback("acmd_display_rotate_view_dialog", "", "View_10 window_1");
            maсroBuilder.ValueChange("gr_rotate_view", "gr_rotate_view_angle", "90.000000000000");
            maсroBuilder.PushButton("gr_rotate_view_rotate", "gr_rotate_view");
            maсroBuilder.PushButton("gr_rotate_view_cancel", "gr_rotate_view");

            TeklaStructures.Connect();

            maсroBuilder.Run();
        }
    }
}

