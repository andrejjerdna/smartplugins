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
using TSD = Tekla.Structures.Drawing;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            if (!drawingHandler.GetConnectionStatus()) return;

            var picker = drawingHandler.GetPicker();

            var pick = picker.PickPoint("");

            var pointlist = pick.Item1;
            var viewbase = pick.Item2;

            TSD.View v = viewbase as TSD.View;

            v.RotateViewOnDrawingPlane(45);
            //v.Modify();
           // viewbase.Modify();

            drawingHandler.GetConnectionStatus();
        }
    }
}
