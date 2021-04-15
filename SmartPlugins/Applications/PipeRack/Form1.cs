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

        public Form1()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else
            {
                InitializeComponent();
            }
        }
        
        public void Button1_Click(object sender, EventArgs e)
        {
            int yarus_count = int.Parse(Yarus_count.Text);
            double count_column = double.Parse(Count_column.Text);


            double razdv_1_2 = double.Parse(Razdv_1_2.Text);
            double razdv_2_3 = double.Parse(Razdv_2_3.Text);
            double b_H1 = double.Parse(B_H1.Text);
            double b_H2 = double.Parse(B_H2.Text);
            double b_H3 = double.Parse(B_H3.Text);
            string b_1_prof = "I30K1_20_93";
            string b_2_prof = "I30K1_20_93";
            string b_3_prof = "I30K1_20_93"; 

            var b_H22 = b_H2 + b_H1;
            var b_H33 = b_H2 + b_H3+ b_H1;



           List<double> Traversy = new List<double>
            {
                b_H1,
                b_H22,
                b_H33
            };
         
            List<string> Traversy_prof = new List<string>
            {
                b_1_prof,
                b_2_prof,
                b_3_prof
            };


            // определение направления
            double x_start = double.Parse(X_start.Text);
            double y_start = double.Parse(Y_start.Text);
            double z_start = double.Parse(Z_start.Text);

            double x_start2 = double.Parse(X_start2.Text);
            double y_start2 = double.Parse(Y_start2.Text);
            double z_start2 = double.Parse(Z_start2.Text);

            //-------------------------

            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);

            T3D.Point CS_point = new T3D.Point(x_start, y_start, z_start);
            T3D.Point CS_point_end = new T3D.Point(x_start2, y_start2, z_start2);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, CS_point, CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);


            var frame = new Frame(yarus_count, count_column)
            {
                Razdv_1_2 = razdv_1_2,
                Razdv_2_3 = razdv_2_3,

                Traversy = Traversy,
                Profiles = Traversy_prof
            };


            frame.Create_rama();



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

        private void Button3_Click(object sender, EventArgs e)
        {
            new Form_att().ShowDialog();

        }



        private void TabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label31_Click(object sender, EventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void Label26_Click(object sender, EventArgs e)
        {

        }

        private void Label28_Click(object sender, EventArgs e)
        {

        }

        private void Label29_Click(object sender, EventArgs e)
        {

        }

        private void Label30_Click(object sender, EventArgs e)
        {

        }

        private void Label22_Click(object sender, EventArgs e)
        {

        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label23_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }


    }
}
