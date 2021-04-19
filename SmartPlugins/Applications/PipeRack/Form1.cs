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
        public bool Nali4ieAtt(List<Attributes> Att, int yarus_count, string Mesaga)
        {
            for (int _count = 1; _count <= yarus_count; _count++)
            {
                if (Att[_count - 1] == null)
                {
                    MessageBox.Show("Введите атрибуты для " + Mesaga.ToString() +" "+ (_count).ToString());
                    return false;
                }

            }
            return true;
        }

        public void Button1_Click(object sender, EventArgs e)
        {
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

            if (!Nali4ieAtt(_attributesColumn, count_column, "колонны")) return;
            if (!Nali4ieAtt(_attributes, yarus_count, "яруса")) return;  // проверка наличия атрибутов


            if (count_column == 3)
            {
                if (!Nali4ieAtt(_attributes2, yarus_count, "яруса")) return;
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
            Regen(Traversy, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат
            Regen(Traversy2, yarus_count); // преобразовал расстояния между траверсами в длины от точки начала координат



            TransformationPlane currentPlane = M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            TransformationPlane zero_plane = new TransformationPlane();
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(zero_plane);
            var TP = SmartTransfomationPlane.GetTransformationPlaneTwoPoints(M, CS_point, CS_point_end);
            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(TP);

            List<Frame> FraMES = new List<Frame>(); // список построенных рам

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

                    _M = M,

                   // Traversy = GetZ(tempX),
                };
                frame.Insert();
                FraMES.Add(frame);
            }

            M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentPlane);
            M.CommitChanges();
        }




        //   private List<double> GetZ(double x)
        //  {
        //      ///gjkexftim cgbcjr Z
        //      return ;
        //  }


    }
}
