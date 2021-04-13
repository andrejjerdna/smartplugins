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
    public partial class Form1 : Form, ITraversa
    {
        public int YarusCount { get; set; }

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

      /*  public double Form2()
        {
            double x_start = double.Parse(X_start.Text);
            return x_start;
        }*/


        private void Button1_Click(object sender, EventArgs e)
        {
            double x_start = double.Parse(X_start.Text);
            double y_start = double.Parse(Y_start.Text);
            double z_start = double.Parse(Z_start.Text);
            YarusCount = int.Parse(Yarus_count.Text);
            double count_column = double.Parse(Count_column.Text);
            double razdv_1_2 = double.Parse(Razdv_1_2.Text);
            double razdv_2_3 = double.Parse(Razdv_2_3.Text); 
            string napr = Napr.Text;
            double b_H1 = double.Parse(B_H1.Text);
            double b_H2 = double.Parse(B_H2.Text);
            double b_H3 = double.Parse(B_H3.Text);
            string b_1_prof = B_1_prof.Text;
            string b_2_prof = B_2_prof.Text;
            string b_3_prof = B_3_prof.Text;

            var b_H22 = b_H2 + b_H1;
            var b_H33 = b_H2 + b_H3+ b_H1;

            List <double> Traversy = new List<double>();

            Traversy.Add(b_H1);
            Traversy.Add(b_H22);
            Traversy.Add(b_H33);

            List<string> Traversy_prof = new List<string>();


            Traversy_prof.Add(b_1_prof);
            Traversy_prof.Add(b_2_prof);
            Traversy_prof.Add(b_3_prof);

            T3D.Vector vector_X = new T3D.Vector(0 + 100, 0, 0), vector_XX;
            T3D.Vector vector_Y = new T3D.Vector(0, 0 + 100, 0), vector_YY;
            T3D.Point START = new T3D.Point(0, 0, 0);

            T3D.CoordinateSystem CSGP = new T3D.CoordinateSystem(START, vector_X, vector_Y);
            TransformationPlane gen_plane = new TransformationPlane(CSGP);
            T3D.Matrix BVD = gen_plane.TransformationMatrixToGlobal; // дожать вопрос с выставлением в глобальные координаты - здесь мартица, которую надо использовать
           // MessageBox.Show(BVD.ToString());
            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane(); //сохранили текущий вид

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(gen_plane); //оси модели переставили по началу координад
            

            T3D.Point CS_point = new T3D.Point(x_start, y_start, z_start);

                if (napr == "+X")
                {
                    vector_X = new T3D.Vector(0 + 100, 0, 0);
                    vector_Y = new T3D.Vector(0, 0 + 100, 0);
                }

                else if (napr == "-X")
                {
                    vector_X = new T3D.Vector(0 - 100, 0, 0);
                    vector_Y = new T3D.Vector(0, 0 - 100, 0);
                }

                else if (napr == "+Y")
                {
                    vector_X = new T3D.Vector(0, 0 + 100, 0);
                    vector_Y = new T3D.Vector(0-100, 0, 0);
                }
                else if (napr == "-Y")
                {
                    vector_X = new T3D.Vector(0, 0 - 100, 0);
                    vector_Y = new T3D.Vector(0 + 100, 0, 0);
                }

            vector_YY = vector_Y;
            vector_XX = vector_X;



            
            T3D.CoordinateSystem CoordinateSystem = new T3D.CoordinateSystem(CS_point, vector_XX, vector_YY);
            TransformationPlane localPlane = new TransformationPlane(CoordinateSystem);

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(localPlane);  //вид по выбранному направлению исходя из условия

            

            Program program = new Program();
            program.Create_rama(this, count_column, razdv_1_2, razdv_2_3, Traversy, Traversy_prof);

            M.CommitChanges();

          M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);  // вернули в пользовательский вид
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var picker = new Picker();  
            T3D.Point pickedPoint = picker.PickPoint();
            X_start.Text = pickedPoint.X.ToString();
            Y_start.Text = pickedPoint.Y.ToString();
            Z_start.Text = pickedPoint.Z.ToString();
        }

        private void TabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
