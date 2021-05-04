﻿using System;
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

namespace PipeRack
{
    public partial class Form1 : Form
    {
        Model M = new Model();
        List<Frame> FraMES = new List<Frame>(); // список построенных рам
        List<Beam> Consoles = new List<Beam>();
        public Form1()
        {
            if (!M.GetConnectionStatus())
                MessageBox.Show("Не подключено к моделе");
            else InitializeComponent();
        }

        public void Button1_Click(object sender, EventArgs e)
        {
            FraMES.Clear();
            T3D.Point CS_point = new T3D.Point(double.Parse(X_start.Text), double.Parse(Y_start.Text), double.Parse(Z_start.Text));
            T3D.Point CS_point_end = new T3D.Point(double.Parse(X_start2.Text), double.Parse(Y_start2.Text), double.Parse(Z_start2.Text));

            if (CS_point == CS_point_end)
            {
                MessageBox.Show("Невозможно определить направление построения - координаты одинаковые");
                return;
            }

            int yarus_count = int.Parse(Yarus_count.Text);
            int count_column = int.Parse(Count_column.Text);

            List<Attributes> _attributesColumn = new List<Attributes>() // правые балки при трёх колоннах или балки при бвух колоннах
            {
                _attributeColumn1,
                _attributeColumn2,
                _attributeColumn3,
             };
            List<Attributes> _attributes = new List<Attributes>() // правые балки при трёх колоннах или балки при бвух колоннах
            {
                _attributeYarus1,
                _attributeYarus2,
                _attributeYarus3,
                _attributeYarus4,
                _attributeYarus5,
                _attributeYarus6,
                _attributeYarus7,
             };
            List<Attributes> _attributes2 = new List<Attributes>() // левые балки при трех колоннах
            {
                _AttYarus1L,
                _AttYarus2L,
                _AttYarus3L,
                _AttYarus4L,
                _AttYarus5L,
                _AttYarus6L,
                _AttYarus7L,
             };
            List<Attributes> _attributesProdolnie = new List<Attributes>() // левые балки при трех колоннах
            {
                _Prodovnie1,
                _Prodovnie2,
                _Prodovnie3,
                _Prodovnie4,
                _Prodovnie5,
                _Prodovnie6,
                _Prodovnie7,
             };
            List<Attributes> _attributesTraversyvprovete = new List<Attributes>()
            {
                _travvprolete1,
                _travvprolete2,
                _travvprolete3,
                _travvprolete4,
                _travvprolete5,
                _travvprolete6,
                _travvprolete7,
            };

            if (!Nali4ieAtt(_attributesColumn, count_column, "колонны")) return;
            if (!Nali4ieAtt(_attributes, yarus_count, "яруса")) return;  // проверка наличия атрибутов
            if (!Nali4ieAtt(_attributesProdolnie, yarus_count, "продольных балок яруса")) return;
            if (!Nali4ieAtt(_attributesTraversyvprovete, yarus_count, "траверс в пролете яруса")) return;

            if (count_column == 3)
            {
                if (!Nali4ieAtt(_attributes2, yarus_count, "левого ряда яруса")) return;
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
                    Attributes = _attributes,
                    Attributes2 = _attributes2,
                    AttributeColumn = _attributesColumn,
                    Traversy2 = Traversy2,
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
                {
                    FRAMESS.Add(F);
                }

                var balkiYarusa = new BalkiYarysa(FRAMESS)
                {
                    AttributesProdolnie = _attributesProdolnie,
                    AttributesTraversyvprovete = _attributesTraversyvprovete,
                };
                balkiYarusa.Insert();
            }

            string Y = null , FFF = null;
            double startLabelY = Double.Parse(LabelY.Text);
            string _startLabelY = null;

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




        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Consoles.Clear();
            var consoleH = Double.Parse(ConsoleH.Text);
            var consoleL = Double.Parse(ConsoleL.Text);
           bool checkRight = cBRight1.Checked;

            for (int count = 0; count < FraMES.Count(); count++)
                {
                    var PlatformMaintenance = new PlatformMaintenance(FraMES[count], M)
                    {
                        consoleH = consoleH,
                        consoleL = consoleL,
                        checkRight = checkRight,
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
    }
}
