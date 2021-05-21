using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Solid;
using TSD = Tekla.Structures.Drawing;
using t3d = Tekla.Structures.Geometry3d;
using Tekla.Structures.Geometry3d;
using SmartExtensions;
using Tekla.Structures.ModelInternal;
using Tekla.Structures.Internal;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Model();

            if (!model.CommitChanges())
                return;

            var pick = new Picker();

            var point = pick.PickPoint();

            var face = pick.PickFace();

            if (point == null || face == null)
                return;

            var items = face.GetEnumerator();

            while (items.MoveNext())
            {
                var ii = items.Current as InputItem;

                if (ii.GetInputType() == InputItem.InputTypeEnum.INPUT_POLYGON)
                {
                    var pointsArray = ii.GetData() as ArrayList;

                    var points = pointsArray.OfType<t3d.Point>().ToList();

                    if (points.Count < 3)
                        break;

                    var workCS = new CoordinateSystem
                    {
                        Origin = points.First(),
                        AxisX = new Vector(points[1] - points[0]),
                        AxisY = new Vector(points.Last() - points[0])
                    };

                    var geomPlane = new GeometricPlane(workCS);

                    var dist = Distance.PointToPlane(point, geomPlane);

                    textBox1.Text = dist.ToString();

                    var gd = new GraphicsDrawer();

                    var userInputDist = Math.Round(dist, 3);

                    gd.DrawText(point, userInputDist.ToString(), new Tekla.Structures.Model.UI.Color());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 14, 15, 16, 17, 21, 22, 23, 30, 31 };

            list.Add(list.Last());

            var result = new List<string>();

            var tempValue = new List<int>();

            for (int i = 0; i < list.Count - 1; i++)
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

                if (i == list.Count - 2)
                {
                    if (tempValue.Count > 1)
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

        private void button3_Click(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            if (!drawingHandler.GetConnectionStatus()) return;

            var picker = drawingHandler.GetPicker();

            var pick = picker.PickObject("");

            var pointlist = pick.Item1;
            var viewbase = pick.Item2;

            TSD.View v = viewbase as TSD.View;

            var objs = viewbase.GetAllObjects().ToIEnumerable<Plugin>().ToList();

            foreach(var pl in objs)
            {
                //Tekla.Structures.DrawingInternal.Operation.ex
            }

                //v.Modify();
                // viewbase.Modify();

                drawingHandler.GetConnectionStatus();
        }
    }
}
