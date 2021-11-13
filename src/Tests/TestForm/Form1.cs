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
using Tekla.Structures.Model;
using tsm =Tekla.Structures.Model;
using t3d = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

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
            var model = new Model();
            var dh = new DrawingHandler();
            var drawings = dh.GetDrawings();

            foreach(var drawing in drawings)
            {
                var asd = drawing as AssemblyDrawing;

                if (asd == null)
                    continue;

                var assemblyModel = model.SelectModelObject(asd.AssemblyIdentifier) as tsm.Assembly;

                if (assemblyModel == null)
                    continue;

                var mainPart = assemblyModel.GetMainPart() as tsm.Part;

                if (mainPart == null)
                    continue;

                var mainPartName = mainPart.Name;

                asd.Name = mainPartName;
                asd.Modify();
            }

            model.CommitChanges();
        }
    }
}
