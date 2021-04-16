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
using T3D = Tekla.Structures.Geometry3d;
using SmartGeometry;

namespace PipeRack
{
    public partial class Form1 : Form
    {
        Model M = new Model();

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

    public Form1()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else
            {
                InitializeComponent();
            }
        }

        public List<double> Regen(List<double> Travs, int yarus_count)
        {
            List<double> Trav = Travs;
            for (int _count = 1; _count< yarus_count; _count++)
            {
                Trav[0]= Trav[0];
                Trav[_count] = Trav[_count-1] + Trav[_count];
            }
            return Trav;
        }

    public void Button1_Click(object sender, EventArgs e)
        {
            List<Attributes> _attributes = new List<Attributes>()
            {
               _attributeYarus1,
                _attributeYarus2,
                _attributeYarus3,
                _attributeYarus4,
                _attributeYarus5,
                _attributeYarus6,
                _attributeYarus7,
                _attributeColumn1,
                _attributeColumn2,
                _attributeColumn3
             };

            int yarus_count = int.Parse(Yarus_count.Text);
            int count_column = int.Parse(Count_column.Text);

            double shagRam = double.Parse(ShagRam.Text);
            double razdv_1_2 = double.Parse(Razdv_1_2.Text);
            double razdv_2_3 = double.Parse(Razdv_2_3.Text);

            List<double> Traversy = new List<double>
            {
                double.Parse(B_H1.Text),
                double.Parse(B_H2.Text),
                double.Parse(B_H3.Text),
                double.Parse(B_H4.Text),
                double.Parse(B_H5.Text),
                double.Parse(B_H6.Text),
                double.Parse(B_H7.Text)
            };

            Regen(Traversy, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат
 
            double x_start = double.Parse(X_start.Text);
            double y_start = double.Parse(Y_start.Text);
            double z_start = double.Parse(Z_start.Text);

            double x_start2 = double.Parse(X_start2.Text);
            double y_start2 = double.Parse(Y_start2.Text);
            double z_start2 = double.Parse(Z_start2.Text);


            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);
            T3D.Point CS_point = new T3D.Point(x_start, y_start, z_start);
            T3D.Point CS_point_end = new T3D.Point(x_start2, y_start2, z_start2);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, CS_point, CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);

 
               for (int _count = 0; _count < shagRam; _count++)
               {
                    var frame = new Frame(yarus_count, count_column)
                    {
                        Razdv_1_2 = razdv_1_2,
                        Razdv_2_3 = razdv_2_3,

                        Traversy = Traversy,
                        Attributes = _attributes,
                    };
                    var G = shagRam * 100 * _count;
                        if (count_column == 2)
                    {
                        frame.CreateRamaDveKolony(G);
                        frame.SetAtt(frame._Columns[0], _attributeColumn1);
                        frame.SetAtt(frame._Columns[1], _attributeColumn2);
                    }

                    else
                {
                        frame.CreateRamaDveKolony(G);
                        frame.SetAtt(frame._Columns[0], _attributeColumn1);
                        frame.SetAtt(frame._Columns[1], _attributeColumn2);
                        frame.SetAtt(frame._Columns[2], _attributeColumn2);
                }
 

            }

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();
        }

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
    }
}
