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
            var views = new DrawingHandler().GetDrawings()
                .ToIEnumerable<GADrawing>()
                .Where(d => !d.Name.ToLower().Contains("титул"))
                .SelectMany(d => d.GetSheet().GetViews().ToIEnumerable<View>()
                .Where(v => v.ViewType != View.ViewTypes.SectionView || v.ViewType != View.ViewTypes.DetailView));

            var TypeFilter = new Type[] { typeof(Tekla.Structures.Drawing.Part) };

            foreach (var view in views)
            {
                var parts = view.GetAllObjects(TypeFilter).ToIEnumerable<Tekla.Structures.Drawing.Part>();

                foreach (var part in parts)
                {
                    if (!part.Hideable.IsHidden)
                    {
                        AssignValues(part, 1, view.Name);
                    }
                }
            }
        }

        private void AssignValues(Tekla.Structures.Drawing.Part ass, double nmbr, string viewName)
        {
            //Получил ID и привел к балке по ID
            System.Int32 atrs = ass.ModelIdentifier.ID;
            Beam part = new Beam();
            part.Identifier.ID = atrs;
            string assPos = string.Empty;
            //Таким обр после приведения я могу запросить GUID 
            part.GetReportProperty("ASSEMBLY.GUID", ref assPos);

            string value = $"л./s. {nmbr}: {viewName}";
            if (!objctsInDrws.ContainsKey(assPos))
            {
                objctsInDrws.Add(assPos, new List<string> { value });
            }
            else
            {
                //Конкретная сборка может 
                //располоагаться и на другом виде и на другом чертеже, поэтому 
                //добавляем для конкретной сборки список имен видов исключая повтор
                if (!objctsInDrws[assPos].Contains(value))
                {
                    objctsInDrws[assPos].Add(value);
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
            var drawingHandler = new DrawingHandler();

            var currentDrawing = drawingHandler.GetActiveDrawing();

            if (currentDrawing != null)
            {
                var drawingObjectEnumerator = currentDrawing.GetSheet().GetAllObjects(typeof(StraightDimension)).ToIEnumerable<StraightDimension>().ToList();
            }
            }
    }
}

