using SmartExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace SmartMacroses.Commecial
{
    public partial class CopyParametersRebarSetsWindow : Form
    {
        private RebarSet _rebarSetOrigin { get; set; }
        private Model _model { get; set; }

        public CopyParametersRebarSetsWindow()
        {
            InitializeComponent();
            TopMost = true;

            comboBox1.SelectedIndex = 0;

            _model = new Model();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Model();

            _rebarSetOrigin = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_REINFORCEMENT) as RebarSet;

            if (_rebarSetOrigin == null)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rebarSets = new ModelObjectSelector().GetSelectedObjects().ToIEnumerable<RebarSet>().ToList();

            if (rebarSets.Count == 0)
            {
                MessageBox.Show("Не выбрано арматурных групп!");
                return;
            }

            progressBar1.Maximum = rebarSets.Count();
            progressBar1.Value = 0;

            foreach (var rebarSet in rebarSets)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    rebarSet.LayerOrderNumber = _rebarSetOrigin.LayerOrderNumber;
                    rebarSet.RebarProperties = _rebarSetOrigin.RebarProperties;
                }

                var count = 0;

                foreach (var guidLine in rebarSet.Guidelines)
                {
                    try
                    {
                        guidLine.Spacing = _rebarSetOrigin.Guidelines[count].Spacing;
                    }
                    catch
                    {

                    }

                    count++;
                }

                rebarSet.Modify();

                progressBar1.Value++;
            }

            _model.CommitChanges();

            MessageBox.Show("Выполнено!");
        }
    }
}
