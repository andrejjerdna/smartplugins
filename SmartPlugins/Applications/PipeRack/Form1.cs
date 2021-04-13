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
            string b_1_prof = B_1_prof.Text;
            string b_2_prof = B_2_prof.Text;
            string b_3_prof = B_3_prof.Text; 

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

 
            var frame = new Frame(yarus_count, count_column)
            {
                Razdv_1_2 = razdv_1_2,
                Razdv_2_3 = razdv_2_3,

                Traversy = Traversy,
                Profiles = Traversy_prof
            };

            // определение направления
            double x_start = double.Parse(X_start.Text);
            double y_start = double.Parse(Y_start.Text);
            double z_start = double.Parse(Z_start.Text);

            double x_start2 = double.Parse(X_start2.Text);
            double y_start2 = double.Parse(Y_start2.Text);
            double z_start2 = double.Parse(Z_start2.Text);

            //-------------------------
            var dir = new Direction()
            {
                X_start = x_start,
                Y_start = y_start,
                Z_start = z_start,
                X_start2 = x_start2,
                Y_start2 = y_start2,
                Z_start2 = z_start2,
            };


            dir.Set_plan();
            frame.Create_rama();
            dir.Return_plan();
         


            M.CommitChanges();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane); //оси модели переставили по началу координад

            var picker = new Picker();  
            T3D.Point pickedPoint = picker.PickPoint("Первая точка");
            
            X_start.Text = pickedPoint.X.ToString();
            Y_start.Text = pickedPoint.Y.ToString();
            Z_start.Text = pickedPoint.Z.ToString();

            var picker2 = new Picker();
            T3D.Point pickedPoint2 = picker2.PickPoint("Вторая точка точка");

            X_start2.Text = pickedPoint2.X.ToString();
            Y_start2.Text = pickedPoint2.Y.ToString();
            Z_start2.Text = pickedPoint2.Z.ToString();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);


        }


        private void TabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
