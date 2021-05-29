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
using SmartTeklaModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PipeRack
{
    public partial class Form1 : Form
    {
        Model M = new Model();                  // текущая модель
        List<Frame> FraMES = new List<Frame>(); // список построенных рам
        List<BalkiYarysa> BalkiYarysA = new List<BalkiYarysa>();


        List<Beam> Consoles = new List<Beam>(); 

        AttributesFrame AttFrame = new AttributesFrame();                   // атрибуты внутри рамы
        AttributesFrameProlet AttFrameProlet = new AttributesFrameProlet(); // атрибуты продольных элементов

        DataGrids DataGrids = new DataGrids();

        public Form1()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else
            {
                InitializeComponent();
                AttFrame.Listovod();
                AttFrameProlet.Listovod();
            }
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            FraMES.Clear();
            BalkiYarysA.Clear();
            AttFrame.Listovod();
            AttFrameProlet.Listovod();
            int yarus_count = int.Parse(Yarus_count.Text);
            int count_column = int.Parse(Count_column.Text);

            var nameOfpipeRack = NameOfPipeRack.Text;

           
            T3D.Point CS_point = new T3D.Point(double.Parse(X_start.Text), double.Parse(Y_start.Text), double.Parse(Z_start.Text));
            T3D.Point CS_point_end = new T3D.Point(double.Parse(X_start2.Text), double.Parse(Y_start2.Text), double.Parse(Z_start2.Text));

            if (CS_point == CS_point_end)
            {
                MessageBox.Show("Невозможно определить направление построения - координаты одинаковые");
                return;
            }

            DataGrids.AddRowDataGrid(dataGridViewYarusRight, AttFrame.AttributesYarusRight);  // считали атрибуты с гридов
            DataGrids.AddRowDataGrid(dataGridViewYarusLeft, AttFrame.AttributesYarusLeft);
            DataGrids.AddRowDataGrid(dataGridViewProdolnieRight, AttFrameProlet.AttProletBeamRight);
            DataGrids.AddRowDataGrid(dataGridViewProdolnieLeft, AttFrameProlet.AttProletBeamLeft);
            DataGrids.AddRowDataGrid(dataGridColumn, AttFrame.AttributesColumn);
            DataGrids.AddRowDataGrid(dataGridViewStoyki, AttFrameProlet.AttProletStoyki);
            DataGrids.AddRowDataGrid(dataGridViewYarusRightVProlete, AttFrameProlet.AttProletTraversaRight);
            DataGrids.AddRowDataGrid(dataGridViewYarusLeftVProlete, AttFrameProlet.AttProletTraversaLeft);

            // проверка наличия атрибутов
            if (!Nali4ieAtt(AttFrame.AttributesColumn, count_column, "колонны")) return;
            if (!Nali4ieAtt(AttFrame.AttributesYarusRight, yarus_count, "яруса")) return;  
            
            if (count_column == 3)
            {
                if (!Nali4ieAtt(AttFrame.AttributesYarusLeft, yarus_count, "левого ряда яруса")) return;
            }

            string shagRam = ShagRam.Text;
            var DistanceList = BoltsMethods.GetDistanceList(shagRam);  // получили список шагов

            double razdv_1_2 = double.Parse(Razdv_1_2.Text);
            double razdv_2_3 = double.Parse(Razdv_2_3.Text);

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

            Regen(Traversy, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат
            Regen(Traversy2, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат

            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, CS_point, CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);

            var tempX = 0.0;

            foreach (var x in DistanceList)
            {
                tempX += x;
                var point = new T3D.Point(tempX, 0, 0);

                var frame = new Frame(M, point, yarus_count, count_column, nameOfpipeRack)
                {
                    Razdv_1_2 = razdv_1_2,
                    Razdv_2_3 = razdv_2_3,
                    Traversy = Traversy,
                    Traversy2 = Traversy2,
                    Attributes = AttFrame.AttributesYarusRight,
                    Attributes2 = AttFrame.AttributesYarusLeft,
                    AttributeColumn = AttFrame.AttributesColumn,
                    UklonbI = UklonbI,
                    _M = M,
                };
                frame.Insert();             // построились рамы (колонны и траверсы)
                FraMES.Add(frame);
            }
            UserAttsFrame(FraMES, nameOfpipeRack);

            for (int count = 0; count < FraMES.Count() - 1; count++)  //выбрал пару рам между которыми буду строить продольные балки
            {
                var FF = FraMES.Skip(count);
                List<Frame> FRAMESS = new List<Frame>();
                foreach (Frame F in FF)
                    FRAMESS.Add(F);
                
                var balkiYarusa = new BalkiYarysa(FRAMESS)
                {
                    AttributesProdolnieRight = AttFrameProlet.AttProletBeamRight,
                    AttributesProdolnieLeft = AttFrameProlet.AttProletBeamLeft,
                    AttributesTraversyvproveteRight = AttFrameProlet.AttProletTraversaRight,
                    AttributesTraversyvproveteLeft = AttFrameProlet.AttProletTraversaLeft,
                    AttributesStoyki = AttFrameProlet.AttProletStoyki,
                };
                balkiYarusa.Insert();
                BalkiYarysA.Add(balkiYarusa);
            }
            UserAttsBalkiYarysa(BalkiYarysA, nameOfpipeRack);


            string Y = null , FFF = null, _startLabelY = null;
            double startLabelY = Double.Parse(LabelY.Text);

            if (count_column == 2)
                Y = (-1 * razdv_1_2).ToString() + " " + (razdv_1_2 + razdv_2_3).ToString();
            else 
                Y = (-1 * razdv_1_2).ToString() + " " +razdv_1_2.ToString() +" "+ (razdv_2_3).ToString();

            for (int _count1 = 0; _count1 < yarus_count; _count1++)
                FFF = FFF + " "+ Traversy[_count1].ToString() ;

            for (int _count1 = 0; _count1 < FraMES.Count(); _count1++)
                _startLabelY += (startLabelY + _count1).ToString() + " ";
                
            Grid Grid = new Grid()
            {
                CoordinateX = shagRam,
                CoordinateY = Y,
                CoordinateZ = "0 " + FFF,
                LabelX = _startLabelY,
                LabelY = LabelX.Text,
                LabelZ = "0 " + FFF,
                ExtensionLeftX = 2000.0,
                ExtensionLeftY = 2000.0,
                ExtensionLeftZ = 2000.0,
                ExtensionRightX = 2000.0,
                ExtensionRightY = 2000.0,
                ExtensionRightZ = 2000.0,
                IsMagnetic = false,
            };
            Grid.Insert();
            Grid.Origin.Z = CS_point.Z;
            Grid.Modify();

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();

            var jsonString = JsonConvert.SerializeObject(FraMES);

            var path = M.GetInfo().ModelPath + "\\frames.json";

            var dirInfo = new DirectoryInfo(M.GetInfo().ModelPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            using (var file = new StreamWriter(path, false))
            {
                file.WriteLine(jsonString);
            }


        }

        private void Button3_Click(object sender, EventArgs e) //построение площадок обслуживания
        {
            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            Consoles.Clear();
            var consoleH = Double.Parse(ConsoleH.Text);
            var consoleL = Double.Parse(ConsoleL.Text);
            var yklonMP = Double.Parse(YklonMP.Text);
           bool checkRight = cBRight1.Checked;

            for (int count = 0; count < FraMES.Count(); count++)
                {
                    var PlatformMaintenance = new PlatformMaintenance(FraMES[count], M)
                    {
                        consoleH = consoleH,
                        consoleL = consoleL,
                        checkRight = checkRight,
                        yklonMP = yklonMP,
                    };
                    PlatformMaintenance.InsertConsole();
                Consoles.Add(PlatformMaintenance.Console);
                }

            for (int count = 0; count < FraMES.Count()-1; count++)
                {
                    var FF = FraMES.Skip(count);
                    List<Frame> FraMESS = new List<Frame>();
                    foreach (Frame F in FF)
                    {
                        FraMESS.Add(F);
                    }

                    var FFF = Consoles.Skip(count);
                    List<Beam> Consoless = new List<Beam>();
                    foreach (Beam F in FFF)
                    {
                        Consoless.Add(F);
                    }

                    var PlatformMaintenance2 = new PlatformMaintenance(FraMESS, Consoless, M)
                        {
                            consoleH = consoleH,
                            consoleL = consoleL,
                            checkRight = checkRight,
                        
                    };
                        PlatformMaintenance2.InsertBalkiPloshadki();
                }
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();
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
        public bool Nali4ieAtt(List<Attributes> Att, int yarus_count, string Mesaga)
        {
            for (int _count = 1; _count <= yarus_count; _count++)
            {
                if (Att[_count - 1] == null)
                {
                    MessageBox.Show("Введите атрибуты для " + Mesaga.ToString() + " " + (_count).ToString());
                    return false;
                }
            }
            return true;
        }

        private void Button15_Click_1(object sender, EventArgs e) // запуск модуля редактирования
        {
            var Redaсtion = new FormRedaсtion();
            Redaсtion.ShowDialog();
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

        private void Count_column_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex == 0)
                pictureBox2.Image = imageList1.Images[0];
            else
                pictureBox2.Image = imageList1.Images[1];
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

        private void UserAttsFrame (List<Frame> FraMES, string RNazvanie)
        {
            for (int i = 0; i< FraMES.Count(); i++)
            {
                foreach (Beam beam in FraMES[i]._Columns)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Колонны");
                    beam.SetUserProperty("RNumberOfSpan", (i+1).ToString());
                }
                foreach (Beam beam in FraMES[i]._Travers)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы яруса");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
            }
        }

        private void UserAttsBalkiYarysa(List<BalkiYarysa> BalkiYarysA, string RNazvanie)
        {
            for (int i = 0; i < BalkiYarysA.Count(); i++)
            {
                foreach (Beam beam in BalkiYarysA[i]._balki)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Продольные балки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
                foreach (Beam beam in BalkiYarysA[i]._balkiLeft)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Продольные балки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
                foreach (Beam beam in BalkiYarysA[i]._stoiki)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Стойки");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
                foreach (Beam beam in BalkiYarysA[i]._traversyvprovete)
                {
                    beam.SetUserProperty("RNazvanie", RNazvanie);
                    beam.SetUserProperty("RType", "Траверсы в пролете");
                    beam.SetUserProperty("RNumberOfSpan", (i + 1).ToString());
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var jsonString = File.ReadAllText(M.GetInfo().ModelPath + "\\frames.json");
            //var result = JsonConvert.DeserializeObject<List<Frame>>(jsonString);
            var result = JsonSerializer.Deserialize<List<Frame>>(jsonString);
        }
    }
}
