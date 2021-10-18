using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;

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

            var rein = obj.GetReinforcements().ToIEnumerable<ModelObject>().ToList();
        }
    }
}

