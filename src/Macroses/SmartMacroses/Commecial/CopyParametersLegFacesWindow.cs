using SmartPlugins.Common.TeklaLibrary.Extensions;
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

namespace SmartMacroses
{
    public partial class CopyParametersLegFacesWindow : Form
    {
        private Model _model { get; set; }
        private Events _modelEvents { get; set; }
        private CopyParametersLegFaces _copyParametersLegFaces { get; set; }

        private double _delta;
        private bool _reversed;
        private int _layerOrderNumber;

        public CopyParametersLegFacesWindow()
        {
            InitializeComponent();
            TopMost = true;

            _model = new Model();
        }

        private void GetParameters(RebarSet rebarSet)
        {
            var rebarLegFace = rebarSet.LegFaces.First();

            Delta.Text = rebarLegFace.AdditonalOffset.ToString();

            if (rebarLegFace.Reversed)
                Flip.SelectedIndex = 1;
            else
                Flip.SelectedIndex = 0;

            Number.Text = rebarLegFace.LayerOrderNumber.ToString();

            _copyParametersLegFaces = new CopyParametersLegFaces(rebarLegFace.AdditonalOffset, rebarLegFace.Reversed, rebarLegFace.LayerOrderNumber);
        }

        private void SetParameterAdditonalOffset()
        {
            var additonalOffset = 0.0;

            Delta.Text = Delta.Text.Replace(".", ",");

            var check1 = double.TryParse(Delta.Text, out additonalOffset);

            if (!check1)
                MessageBox.Show("Не верно введено значение смещения!");

            if (!check1)
                return;

            _delta = additonalOffset;
        }

        private void SetParameterLayerOrderNumber()
        {
            var layerOrderNumber = 1;

            Number.Text = Number.Text.Replace(".", ",");

            var check1 = int.TryParse(Number.Text, out layerOrderNumber);

            if (!check1)
                MessageBox.Show("Не верно введен номер слоя!");

            if (!check1)
                return;

            _layerOrderNumber = layerOrderNumber;
        }

        private void SetParameterReversed()
        {
            _reversed = false;

            if (Flip.SelectedIndex == 1)
                _reversed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rebarSets = new ModelObjectSelector().GetSelectedObjects().ToIEnumerable<RebarSet>().ToList();

            _copyParametersLegFaces = new CopyParametersLegFaces(_delta, _reversed, _layerOrderNumber);

            var currenValue = 0;

            progressBar1.Maximum = rebarSets.Count;
            progressBar1.Value = currenValue;

            foreach (var rebarSet in rebarSets)
            {
                var legFaces = rebarSet.LegFaces;

                foreach(var legFace in legFaces)
                {
                    legFace.AdditonalOffset = _copyParametersLegFaces.Delta;
                    legFace.Reversed = _copyParametersLegFaces.Reversed;
                    legFace.LayerOrderNumber = _copyParametersLegFaces.LayerOrderNumber;
                }

                rebarSet.Modify();

                currenValue = +1;
                progressBar1.Value = currenValue;
            }

            progressBar1.Value = progressBar1.Maximum;

            _model.CommitChanges();

            MessageBox.Show("Выполнено!");
        }

        private void Delta_TextChanged(object sender, EventArgs e)
        {
            SetParameterAdditonalOffset();
        }

        private void Flip_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetParameterReversed();
        }

        private void Number_TextChanged(object sender, EventArgs e)
        {
            SetParameterLayerOrderNumber();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mo = new Picker().PickObject(Picker.PickObjectEnum.PICK_ONE_REINFORCEMENT);
            
                if (mo is RebarSet rebarSet)
                    GetParameters(rebarSet);
        }

        private void CopyParametersLegFacesWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
