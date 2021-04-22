using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using T3D = Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public partial class Form1 : Form
    {
        private Attributes _attributeYarus1;
        private Attributes _attributeYarus2;
        private Attributes _attributeYarus3;
        private Attributes _attributeYarus4;
        private Attributes _attributeYarus5;
        private Attributes _attributeYarus6;
        private Attributes _attributeYarus7;

        private Attributes _attributeColumn1;
        private Attributes _attributeColumn2;
        private Attributes _attributeColumn3;

        private Attributes _AttYarus1L;
        private Attributes _AttYarus2L;
        private Attributes _AttYarus3L;
        private Attributes _AttYarus4L;
        private Attributes _AttYarus5L;
        private Attributes _AttYarus6L;
        private Attributes _AttYarus7L;

        private Attributes _Prodovnie1;
        private Attributes _Prodovnie2;
        private Attributes _Prodovnie3;
        private Attributes _Prodovnie4;
        private Attributes _Prodovnie5;
        private Attributes _Prodovnie6;
        private Attributes _Prodovnie7;

        private Attributes _travvprolete1;
        private Attributes _travvprolete2;
        private Attributes _travvprolete3;
        private Attributes _travvprolete4;
        private Attributes _travvprolete5;
        private Attributes _travvprolete6;
        private Attributes _travvprolete7;

        private void Button2_Click(object sender, EventArgs e)
        {
            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane); //оси модели переставили по началу координад

            var picker = new Picker();
            T3D.Point pickedPoint = picker.PickPoint("Первая точка в глобальных координатах");

            X_start.Text = pickedPoint.X.ToString();
            Y_start.Text = pickedPoint.Y.ToString();
            Z_start.Text = pickedPoint.Z.ToString();

            var picker2 = new Picker();
            T3D.Point pickedPoint2 = picker2.PickPoint("Вторая точка точка в глобальных координатах");

            X_start2.Text = pickedPoint2.X.ToString();
            Y_start2.Text = pickedPoint2.Y.ToString();
            Z_start2.Text = pickedPoint2.Z.ToString();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
        }
        private void AttYarus1_Click(object sender, EventArgs e)
        {
            var AttYorus1 = new Form_att(_attributeYarus1);
            AttYorus1.ShowDialog();
            _attributeYarus1 = AttYorus1.GetAttributes();

        }

        private void AttYarus2_Click(object sender, EventArgs e)
        {
            var AttYorus2 = new Form_att(_attributeYarus2);
            AttYorus2.ShowDialog();
            _attributeYarus2 = AttYorus2.GetAttributes();
        }

        private void AttYarus3_Click(object sender, EventArgs e)
        {
            var AttYorus3 = new Form_att(_attributeYarus3);
            AttYorus3.ShowDialog();
            _attributeYarus3 = AttYorus3.GetAttributes();
        }

        private void AttYarus4_Click(object sender, EventArgs e)
        {
            var AttYorus4 = new Form_att(_attributeYarus4);
            AttYorus4.ShowDialog();
            _attributeYarus4 = AttYorus4.GetAttributes();
        }

        private void AttYarus5_Click(object sender, EventArgs e)
        {
            var AttYorus5 = new Form_att(_attributeYarus5);
            AttYorus5.ShowDialog();
            _attributeYarus5 = AttYorus5.GetAttributes();
        }

        private void AttYarus6_Click(object sender, EventArgs e)
        {
            var AttYorus6 = new Form_att(_attributeYarus6);
            AttYorus6.ShowDialog();
            _attributeYarus6 = AttYorus6.GetAttributes();
        }

        private void AttYarus7_Click(object sender, EventArgs e)
        {
            var AttYorus7 = new Form_att(_attributeYarus7);
            AttYorus7.ShowDialog();
            _attributeYarus7 = AttYorus7.GetAttributes();
        }

        private void AttColumn1_Click(object sender, EventArgs e)
        {
            var AttCol1 = new Form_att(_attributeColumn1);
            AttCol1.ShowDialog();
            _attributeColumn1 = AttCol1.GetAttributes();
        }

        private void AttColumn2_Click(object sender, EventArgs e)
        {
            var AttCol2 = new Form_att(_attributeColumn2);
            AttCol2.ShowDialog();
            _attributeColumn2 = AttCol2.GetAttributes();
        }

        private void AttColumn3_Click(object sender, EventArgs e)
        {
            var AttCol3 = new Form_att(_attributeColumn3);
            AttCol3.ShowDialog();
            _attributeColumn3 = AttCol3.GetAttributes();
        }
        private void AttYarus1L_Click(object sender, EventArgs e)
        {
            var AttYorus1l = new Form_att(_AttYarus1L);
            AttYorus1l.ShowDialog();
            _AttYarus1L = AttYorus1l.GetAttributes();
        }

        private void AttYarus2L_Click(object sender, EventArgs e)
        {
            var AttYorus2l = new Form_att(_AttYarus2L);
            AttYorus2l.ShowDialog();
            _AttYarus2L = AttYorus2l.GetAttributes();
        }

        private void AttYarus3L_Click(object sender, EventArgs e)
        {
            var AttYorus3l = new Form_att(_AttYarus3L);
            AttYorus3l.ShowDialog();
            _AttYarus3L = AttYorus3l.GetAttributes();
        }

        private void AttYarus4L_Click(object sender, EventArgs e)
        {
            var AttYorus4l = new Form_att(_AttYarus4L);
            AttYorus4l.ShowDialog();
            _AttYarus4L = AttYorus4l.GetAttributes();
        }

        private void AttYarus5L_Click(object sender, EventArgs e)
        {
            var AttYorus5l = new Form_att(_AttYarus5L);
            AttYorus5l.ShowDialog();
            _AttYarus5L = AttYorus5l.GetAttributes();
        }

        private void AttYarus6L_Click(object sender, EventArgs e)
        {
            var AttYorus6l = new Form_att(_AttYarus6L);
            AttYorus6l.ShowDialog();
            _AttYarus6L = AttYorus6l.GetAttributes();
        }

        private void AttYarus7L_Click(object sender, EventArgs e)
        {
            var AttYorus7l = new Form_att(_AttYarus7L);
            AttYorus7l.ShowDialog();
            _AttYarus7L = AttYorus7l.GetAttributes();
        }
    }
}
