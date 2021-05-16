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

namespace PipeRack
{
    public partial class Form1 : Form
    {
        Model M = new Model();                  // текущая модель
        List<Frame> FraMES = new List<Frame>(); // список построенных рам
        List<Beam> Consoles = new List<Beam>(); 

        AttributesFrame AttFrame = new AttributesFrame();                   // атрибуты внутри рамы
        AttributesFrameProlet AttFrameProlet = new AttributesFrameProlet(); // атрибуты продольных элементов

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
            AttFrame.Listovod();
            AttFrameProlet.Listovod();
            int yarus_count = int.Parse(Yarus_count.Text);
            int count_column = int.Parse(Count_column.Text);

            T3D.Point CS_point = new T3D.Point(double.Parse(X_start.Text), double.Parse(Y_start.Text), double.Parse(Z_start.Text));
            T3D.Point CS_point_end = new T3D.Point(double.Parse(X_start2.Text), double.Parse(Y_start2.Text), double.Parse(Z_start2.Text));

            if (CS_point == CS_point_end)
            {
                MessageBox.Show("Невозможно определить направление построения - координаты одинаковые");
                return;
            }

            AddRowDataGrid(dataGridViewYarusRight, AttFrame.AttributesYarusRight);  // считали атрибуты с гридов
            AddRowDataGrid(dataGridViewYarusLeft, AttFrame.AttributesYarusLeft);
            AddRowDataGrid(dataGridViewProdolnieRight, AttFrameProlet.AttProletBeamRight);
            AddRowDataGrid(dataGridViewProdolnieLeft, AttFrameProlet.AttProletBeamLeft);
            AddRowDataGrid(dataGridColumn, AttFrame.AttributesColumn);
            AddRowDataGrid(dataGridViewStoyki, AttFrameProlet.AttProletStoyki);

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

                var frame = new Frame(M, point, yarus_count, count_column)
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

            }

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
        }

        private void Button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridViewProdolnieRight, AttFrameProlet.AttProletBeamRight);
        }
        private void Button9_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridViewProdolnieLeft, AttFrameProlet.AttProletBeamLeft);
        }
        private void Button15_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridViewYarusRight, AttFrame.AttributesYarusRight);
        }
        private void AddYarusLeft_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridViewYarusLeft, AttFrame.AttributesYarusLeft);
        }
        private void AddCoiumn_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridColumn, AttFrame.AttributesColumn);
        }
        private void AddStoyki_Click(object sender, EventArgs e)
        {
            WorkWithDataGrid(dataGridViewStoyki, AttFrameProlet.AttProletStoyki);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridViewProdolnieRight);
        }
        private void Button8_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridViewProdolnieLeft);
        }
        private void CopyYarusRight_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridViewYarusRight);
        }
        private void CopyYarusLeft_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridViewYarusLeft);
        }
        private void CopyColumn_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridColumn);
        }
        private void CopyStoyki_Click(object sender, EventArgs e)
        {
            CreateNewRow(dataGridViewStoyki);
        }


        private void WorkWithDataGrid (DataGridView dataGridView1, List<Attributes> _attributesProdolnie)
        {
            var curent = AddRowDataGrid(dataGridView1, _attributesProdolnie);

            if (dataGridView1.Name == dataGridColumn.Name)
            {
                var attr = new Form_att_column(_attributesProdolnie, curent);
                attr.ShowDialog();
                _attributesProdolnie[attr.selectY] = attr.GetAttributes();
                dataGridView1.Rows.Clear();
            }
            else
            {
                var attr = new Form_att(_attributesProdolnie, curent);
                attr.ShowDialog();
                _attributesProdolnie[attr.selectY] = attr.GetAttributes();
                dataGridView1.Rows.Clear();

            }

            for (int _count = 0; _count < _attributesProdolnie.Count; _count++)
            {
                var at = _attributesProdolnie[_count];
                if (at == null)
                    continue;
                else
                    dataGridView1.Rows.Add(_count + 1, at.Name, at.Profile, at.Material, at.Class, at.PrefixSborki, at.NomerSborki, at.PolojenieVertikalno, at.PolojeniePovorot, at.PolojenieGorizontalno);
            }
            dataGridView1.Update();
        }
        private int AddRowDataGrid(DataGridView dataGridView1, List<Attributes> _attributesProdolnie)
        {
            var curent = 0; // конкретный ярус в строке
            var index = 0;  // индекс яруса в конкретной строке
            if (dataGridView1.CurrentRow != null)
                index = dataGridView1.CurrentRow.Index;

            if (dataGridView1[0, index].Value != null)
                curent = Convert.ToInt32(dataGridView1[0, index].Value) - 1;
            else
                curent = 0;

            for (int count_r = 0; count_r < dataGridView1.RowCount - 1; count_r++)
            {
                var nomerProleta = Convert.ToInt32(dataGridView1[0, count_r].Value) - 1;

                if (_attributesProdolnie[nomerProleta] == null)
                    _attributesProdolnie[nomerProleta] = new Attributes();

                AttSetGrid(_attributesProdolnie[nomerProleta], count_r, dataGridView1);
            }
            return curent;
        }
        private void CreateNewRow (DataGridView dataGridView1)
        {
            int I = 0;
            if (dataGridView1.CurrentRow == null)
                return;
            I = dataGridView1.SelectedCells[0].RowIndex;

            dataGridView1.Rows.Add(CloneWithValues(dataGridView1.Rows[I]));
        }


        private void AttSetGrid (Attributes _attributesProdolnie, int I, DataGridView dataGridView1)
        {
            _attributesProdolnie.Name = dataGridView1[1, I].Value.ToString();
            _attributesProdolnie.Profile = dataGridView1[2, I].Value.ToString();
            _attributesProdolnie.Material = dataGridView1[3, I].Value.ToString();
            _attributesProdolnie.Class = dataGridView1[4, I].Value.ToString();
            _attributesProdolnie.PrefixSborki = dataGridView1[5, I].Value.ToString();
            _attributesProdolnie.NomerSborki = dataGridView1[6, I].Value.ToString();
            _attributesProdolnie.PolojenieVertikalno = Convert.ToInt32(dataGridView1[7, I].Value.ToString());
            _attributesProdolnie.PolojeniePovorot = Convert.ToInt32(dataGridView1[8, I].Value.ToString());
            _attributesProdolnie.PolojenieGorizontalno = Convert.ToInt32(dataGridView1[9, I].Value.ToString());
        }
        public DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
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

        private void PictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void Yarus_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
