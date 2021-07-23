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
using Tekla.Structures.Model.Operations;
using SmartExtensions;
using Part = Tekla.Structures.Model.Part;
using Parallel = System.Threading.Tasks.Parallel;
using SmartTeklaModel.Rebar;
using View = Tekla.Structures.Drawing.View;
using Tekla.Structures;
using ModelObject = Tekla.Structures.Drawing.ModelObject;
using Point = Tekla.Structures.Geometry3d.Point;
using SmartGeometry;
using System.IO;
using Size = Tekla.Structures.Drawing.Size;

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
            var model = new Model();
                    var DrawingHandler = new DrawingHandler();

        Size A3 = new Size(410.0, 287.0);
            Drawing newDrawing = new GADrawing("standard", A3);
            newDrawing.Insert();

            var arry = new ArrayList();
            var all = model.GetModelObjectSelector().GetAllObjects();

            while (all.MoveNext())
            {
                if(all.Current is Assembly part)
                arry.Add(part.Identifier);
            }

            Tekla.Structures.Drawing.View MyView = new Tekla.Structures.Drawing.View(newDrawing.GetSheet(),
                                                                         new CoordinateSystem(),
                                                                         new CoordinateSystem(),
                                                                         arry);

            MyView.Name = Name;
            MyView.Insert();

            newDrawing.PlaceViews();

            DrawingHandler.SetActiveDrawing(newDrawing);
            //var model = new Model();
            //var dh = new DrawingHandler();
            //var d = dh.GetActiveDrawing();

            //var views = d.GetSheet().GetObjects().ToIEnumerable<View>().ToList();

            //foreach (var view in views)
            //{
            //    var currentTP = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            //    model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(view.DisplayCoordinateSystem));


            //    var parts = d.GetSheet().GetObjects()
            //        .ToIEnumerable<View>()
            //        .SelectMany(v => v.GetModelObjects().ToIEnumerable<ModelObject>())
            //        .Select(mo => model.SelectModelObject(new Identifier(mo.ModelIdentifier.ID)) as Part)
            //        .Where(p => p != null)
            //        .ToList();

            //    foreach(var part in parts)
            //    {
            //        var mg = new ModelObjectGeometry(model, part);

            //        var p1 = new Point(0, 0, mg.MidZ);
            //        var p2 = new Point(1000, 0, mg.MidZ);
            //        var p3 = new Point(0, 1000, mg.MidZ);

            //        var intersect = part.GetSolid().IntersectAllFaces(p1, p2, p3);
            //    }

            //    model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            //}
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //var list = new List<int> { 1, 2, 3, 4, 5, 6, 9, 10, 14, 15, 16, 17, 21, 22, 23, 30, 31 };

            //list.Add(list.Last());

            //var result = new List<string>();

            //var tempValue = new List<int>();

            //for (int i = 0; i < list.Count - 1; i++)
            //{
            //    var current = list[i];
            //    var next = list[i + 1];

            //    tempValue.Add(current);

            //    var differend = next - current;

            //    if (differend > 1)
            //    {
            //        result.Add(tempValue.First() + "-" + tempValue.Last());
            //        tempValue = new List<int>();
            //    }

            //    if (i == list.Count - 2)
            //    {
            //        if (tempValue.Count > 1)
            //        {
            //            result.Add(tempValue.First() + "-" + tempValue.Last());
            //        }
            //        else
            //        {
            //            result.Add(tempValue.First().ToString());
            //        }
            //    }

            //}

            var path = AppDomain.CurrentDomain.BaseDirectory;

            File.WriteAllText(path, Properties.Resources.test);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var model = new Model();

            if (!model.CommitChanges())
                return;

            var assemblies = model.GetModelObjectSelector()
                .GetAllObjectsWithType(Tekla.Structures.Model.ModelObject.ModelObjectEnum.ASSEMBLY)
                .ToConcurrentBag<Assembly>();

            Parallel.ForEach(assemblies, (assembly) =>
            {
                var num = new RebarNumberator(assembly, "REBAR_SEQ_NO");
                num.RefreshNumbers();
            });

            model.CommitChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var drawingHandler = new DrawingHandler();

            //var currentDrawing = drawingHandler.GetActiveDrawing();

            //if (currentDrawing != null)
            //{
            //    var drawingObjectEnumerator = currentDrawing.GetSheet().GetAllObjects(typeof(Tekla.Structures.Drawing.Part)).ToIEnumerable<Tekla.Structures.Drawing.Part>().ToList();

            //    var d = drawingObjectEnumerator[0].Hideable.IsHidden;
            //}

            var pick = new Picker();
            var mo = pick.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT);

            }
    }
}

