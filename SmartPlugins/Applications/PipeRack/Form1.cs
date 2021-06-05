using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartTeklaModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;
using T3D = Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Newtonsoft.Json;

namespace PipeRack
{
    public partial class Form1 : Form
    {
        Model M = new Model(); // текущая модель
        AttributesFrame AttFrame = new AttributesFrame();                   // атрибуты внутри рамы
        AttributesFrameProlet AttFrameProlet = new AttributesFrameProlet(); // атрибуты продольных элементов
        DataGrids DataGrids = new DataGrids();
        Postroenie StroikaVeka;

        public Form1()
        {
            InitializeComponent();
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            var CS_point = new T3D.Point(double.Parse(X_start.Text), double.Parse(Y_start.Text), double.Parse(Z_start.Text));
            var CS_point_end = new T3D.Point(double.Parse(X_start2.Text), double.Parse(Y_start2.Text), double.Parse(Z_start2.Text));
            
            if (CS_point == CS_point_end)
            {
                MessageBox.Show("Невозможно определить направление построения - координаты одинаковые");
                return;
            }
            string shagRam = ShagRam.Text;
            var DistanceList = BoltsMethods.GetDistanceList(shagRam);  // получили список шагов

            double razdv1to2 = double.Parse(Razdv_1_2.Text);
            double razdv2to3 = double.Parse(Razdv_2_3.Text);

            List<double> Traversy = new List<double>  // шаги траверс по Z
            {
                double.Parse(B_H1.Text),
                double.Parse(B_H2.Text),
                double.Parse(B_H3.Text),
                double.Parse(B_H4.Text),
                double.Parse(B_H5.Text),
                double.Parse(B_H6.Text),
                double.Parse(B_H7.Text)
            };
            List<double> Traversy2 = new List<double>  // шаги траверс по Z
            {
                double.Parse(L_H1.Text),
                double.Parse(L_H2.Text),
                double.Parse(L_H3.Text),
                double.Parse(L_H4.Text),
                double.Parse(L_H5.Text),
                double.Parse(L_H6.Text),
                double.Parse(L_H7.Text)
            };
            List<double> UklonbI = new List<double>
            {
                double.Parse(YklonYarys1.Text),
                double.Parse(YklonYarys2.Text),
                double.Parse(YklonYarys3.Text),
                double.Parse(YklonYarys4.Text),
                double.Parse(YklonYarys5.Text),
                double.Parse(YklonYarys6.Text),
                double.Parse(YklonYarys7.Text)
            };

            var yarus_count = int.Parse(Yarus_count.Text);
            Regen(Traversy, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат
            Regen(Traversy2, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат



            DataGrids.AddRowDataGrid(dataGridViewYarusRight, AttFrame.AttributesYarusRight);  // считали атрибуты с гридов
            DataGrids.AddRowDataGrid(dataGridViewYarusLeft, AttFrame.AttributesYarusLeft);
            DataGrids.AddRowDataGrid(dataGridViewProdolnieRight, AttFrameProlet.AttProletBeamRight);
            DataGrids.AddRowDataGrid(dataGridViewProdolnieLeft, AttFrameProlet.AttProletBeamLeft);
            DataGrids.AddRowDataGrid(dataGridColumn, AttFrame.AttributesColumn);
            DataGrids.AddRowDataGrid(dataGridViewStoyki, AttFrameProlet.AttProletStoyki);
            DataGrids.AddRowDataGrid(dataGridViewYarusRightVProlete, AttFrameProlet.AttProletTraversaRight);
            DataGrids.AddRowDataGrid(dataGridViewYarusLeftVProlete, AttFrameProlet.AttProletTraversaLeft);

            Postroenie StroikaVeka = new Postroenie()
            {
                nameOfpipeRack = NameOfPipeRack.Text,
                yarus_count = yarus_count,
                count_column = int.Parse(Count_column.Text),
                CS_point = CS_point,
                CS_point_end = CS_point_end,
                DistanceList = DistanceList,
                razdv1to2 = razdv1to2,
                razdv2to3 = razdv2to3,
                Traversy = Traversy,
                Traversy2 = Traversy2,
                UklonbI = UklonbI,
                AttFrame = AttFrame,
                AttFrameProlet = AttFrameProlet,
                startLabelY = Double.Parse(LabelY.Text),
                shagRam = shagRam,
                LabelX = LabelX.Text,

            };
            StroikaVeka.Insert();
            this.StroikaVeka = StroikaVeka;
        }

        private void Button3_Click(object sender, EventArgs e) //построение площадок обслуживания
        {
            
            PostroeniePlatform CuperPlatform = new PostroeniePlatform()
            {
                StroikaVeka = StroikaVeka,
                ConsoleH = Double.Parse(ConsoleH.Text),
                ConsoleL = Double.Parse(ConsoleL.Text),
                YklonMP = Double.Parse(YklonMP.Text),
                CheckRight = cBRight1.Checked,
            };

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
        private List<double> Regen(List<double> Travs, int yarus_count)
        {
            List<double> Trav = Travs;
            for (int _count = 1; _count < yarus_count; _count++)
            {
                Trav[0] = Trav[0];
                Trav[_count] = Trav[_count - 1] + Trav[_count];
            }
            return Trav;
        }

        private void Button15_Click_1(object sender, EventArgs e) // запуск модуля редактирования
        {
            var Redaсtion = new FormRedaсtion();
            Redaсtion.ShowDialog();
        }
        private void button16_Click(object sender, EventArgs e)
        {
            var jsonString = File.ReadAllText(M.GetInfo().ModelPath + "\\frames.json");


           // var result = JsonSerializer.Deserialize<List<Frame>>(jsonString);
            var oldFrames = JsonConvert.DeserializeObject<List<Frame>>(jsonString);

            ////oldFrames[0]._basePoint.X = 5000;
            oldFrames[0].Modify();
        }
        

        private void Count_column_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0)
            {
                pictureBox2.Image = imageList1.Images[0];
                L_H7.Hide();
                L_H6.Hide();
                L_H5.Hide();
                L_H4.Hide();
                L_H3.Hide();
                L_H2.Hide();
                L_H1.Hide();
            }
                
            else
            {
                pictureBox2.Image = imageList1.Images[1];
                L_H7.Show();
                L_H6.Show();
                L_H5.Show();
                L_H4.Show();
                L_H3.Show();
                L_H2.Show();
                L_H1.Show();
            }
               

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewProdolnieRight, AttFrameProlet.AttProletBeamRight);
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewProdolnieLeft, AttFrameProlet.AttProletBeamLeft);
        }
        private void Button15_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewYarusRight, AttFrame.AttributesYarusRight);
        }
        private void AddYarusLeft_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewYarusLeft, AttFrame.AttributesYarusLeft);
        }
        private void AddCoiumn_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridColumn, AttFrame.AttributesColumn);
        }
        private void AddStoyki_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewStoyki, AttFrameProlet.AttProletStoyki);
        }
        private void Button14_Click(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewYarusRightVProlete, AttFrameProlet.AttProletTraversaRight);
        }
        private void Button9_Click_1(object sender, EventArgs e)
        {
            DataGrids.WorkWithDataGrid(dataGridViewYarusLeftVProlete, AttFrameProlet.AttProletTraversaLeft);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewProdolnieRight);
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewProdolnieLeft);
        }
        private void CopyYarusRight_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewYarusRight);
        }
        private void CopyYarusLeft_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewYarusLeft);
        }
        private void CopyColumn_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridColumn);
        }
        private void CopyStoyki_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewStoyki);
        }
        private void Button12_Click(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewYarusRightVProlete);
        }
        private void Button8_Click_1(object sender, EventArgs e)
        {
            DataGrids.CreateNewRow(dataGridViewYarusLeftVProlete);
        }

        private void DelRowCol_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridColumn);
        }
        private void Button4_Click_1(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewStoyki);
        }
        private void Button13_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewYarusRight); 
        }
        private void Button10_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewYarusLeft);
        }
        private void Button6_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewProdolnieRight);
        }
        private void Button7_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewProdolnieLeft);
        }
        private void Button11_Click(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewYarusRightVProlete);
        }
        private void Button5_Click_1(object sender, EventArgs e)
        {
            DataGrids.DeleteeNewRow(dataGridViewYarusLeftVProlete);
        }

        

        


    }
}
