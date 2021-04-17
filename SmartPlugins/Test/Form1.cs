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

            var pick = picker.PickObject("");

            var pointlist = pick.Item1;
            var viewbase = pick.Item2;

            TSD.View v = viewbase as TSD.View;

            
            //v.Modify();
            // viewbase.Modify();

            drawingHandler.GetConnectionStatus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 14, 15, 16, 17, 21, 22, 23, 30, 31};

            list.Add(list.Last());

            var result = new List<string>();

            var tempValue = new List<int>();

            for (int i = 0; i < list.Count-1; i++)
            {
                var current = list[i];
                var next = list[i + 1];

                tempValue.Add(current);

                var differend = next - current;

                if (differend > 1)
                {
                    result.Add(tempValue.First() + "-" + tempValue.Last());
                    tempValue = new List<int>();
                }
   
                if(i == list.Count - 2)
                {
                    if(tempValue.Count > 1)
                    {
                        result.Add(tempValue.First() + "-" + tempValue.Last());
                    }
                    else
                    {
                        result.Add(tempValue.First().ToString());
                    }
                }

            }
        }
    }
}
