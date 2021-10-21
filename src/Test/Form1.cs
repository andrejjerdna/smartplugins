using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Autodesk.Navisworks.Api;
using Application = Autodesk.Navisworks.Api.Application;

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
            var p = new Picker();
            var obj = p.PickObject(Picker.PickObjectEnum.PICK_ONE_PART) as Part;

          //  var rein = obj.GetReinforcements().ToIEnumerable<ModelObject>().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var www = Application.ActiveDocument.Models;
        }
    }
}

