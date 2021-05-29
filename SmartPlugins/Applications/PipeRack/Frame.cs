﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using SmartGeometry;
using System.Windows.Forms;

namespace PipeRack
{
    public class Frame
    {
        public List<Attributes> AttributeColumn { get; set; }
        public List<Attributes> Attributes { get; set; }
        public List<Attributes> Attributes2 { get; set; }

        public List<double> Traversy { get; set; }
        public List<double> Traversy2 { get; set; }
        public List<string> Profiles { get; set; }
        public List<double> UklonbI { get; set; }

        public double Razdv_1_2 { get; set; }
        public double Razdv_2_3 { get; set; }

        public Model _M;
        private int _yarusCount;
        private int _count_column;
        private string _nameOfPipeRack;
        public TransformationPlane workTP { get; set; }

        public Point _basePoint;

        public List<Beam> _Columns = new List<Beam>();
        public List<Beam> _Travers = new List<Beam>();
        public List<Beam> _TraversRight = new List<Beam>();
        public List<Beam> _TraversLeft = new List<Beam>();
        public List<double> Z { get; set; }

        private Point ColumnStartPoint1;
        private Point ColumnStartPoint2;
        private Point ColumnStartPoint3;
        private Point ColumnEndPoint1;
        private Point ColumnEndPoint2;
        private Point ColumnEndPoint3;


        public Frame(Model M, Point basePoint, int yarusCount, int count_column, string nameOfpipeRack)
        {
            _basePoint = basePoint;
            _yarusCount = yarusCount;
            _count_column = count_column;
            _M = M;
            _nameOfPipeRack = nameOfpipeRack;
        }
        public Frame()
        {

        }

        public void Insert()
        {
            var currentTP = _M.GetWorkPlaneHandler().GetCurrentTransformationPlane();
            var workCS = new CoordinateSystem(_basePoint, new Vector(1, 0, 0), new Vector(0, 1, 0));
            workTP = new TransformationPlane(workCS);
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(workTP);

            Connections Con = new Connections();

            Points();

            var Columns = new CreateColumn();

            _Columns.Add(Columns.Insert(AttributeColumn[0], ColumnStartPoint1, ColumnEndPoint1));
            _Columns.Add(Columns.Insert(AttributeColumn[1], ColumnStartPoint2, ColumnEndPoint2));
            CreateTraversy(_Columns[0], _Columns[1], Traversy, Attributes, "Right");

            Con.BeamsToColumn(_Columns[0], _TraversRight);
            Con.BeamsToColumn(_Columns[1], _TraversRight);


            if (_count_column == 3)
            {
                //var Column3 = new CreateColumn(AttributeColumn[2], ColumnStartPoint3, ColumnEndPoint3);
                _Columns.Add(Columns.Insert(AttributeColumn[2], ColumnStartPoint3, ColumnEndPoint3));
                CreateTraversy(_Columns[1], _Columns[2], Traversy2, Attributes, "Left");

                Con.BeamsToColumn(_Columns[1], _TraversLeft);
                Con.BeamsToColumn(_Columns[2], _TraversLeft);
            }
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }

        private void CreateTraversy(Beam Column1, Beam Column2, List<double> Traversy, List<Attributes> Attributes, string AR)
        {
            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_1_start = new Point(Column1.StartPoint.X, Column1.StartPoint.Y, Traversy[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                Point B_1_end = new Point(Column2.StartPoint.X, Column2.StartPoint.Y, Traversy[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                if (AR == "Right")
                {
                    _TraversRight.Add(BeamMain(Attributes[_count], B_1_start, B_1_end, (_count + 1).ToString(), AR));
                    _Travers.Add(_TraversRight[_count]);
                }
                if (AR == "Left")
                {
                    _TraversLeft.Add(BeamMain(Attributes[_count], B_1_start, B_1_end, (_count + 1).ToString(), AR));
                    _Travers.Add(_TraversRight[_count]);
                }
            }
        }

        public Beam BeamMain(Attributes attributes, Point startPoint, Point endPoint, string RNumberOfYarus, string DirectionOfYarus)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            SetAtt(newBeam, attributes);

            newBeam.SetUserProperty("RNumberOfYarus", RNumberOfYarus);
            newBeam.SetUserProperty("DirectionOfYarus", DirectionOfYarus);
            newBeam.Modify();
            return newBeam;
        }
        public Beam BeamMain(Point startPoint, Point endPoint)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            return newBeam;
        }

        public void SetAtt(Beam beam, Attributes _attributes)
        {
            if (_attributes != null)
            {
                beam.Name = _attributes.Name;
                beam.Profile.ProfileString = _attributes.Profile;
                beam.Material.MaterialString = _attributes.Material;
                beam.Class = _attributes.Class;
                beam.PartNumber.Prefix = _attributes.PrefixSborki;

                int nomerSborki;
                var H = Int32.TryParse(_attributes.NomerSborki.ToString(), out nomerSborki);
                if (!H)
                {
                    MessageBox.Show("Введено не целое число сборки");
                    return;
                }

                beam.PartNumber.StartNumber = nomerSborki;

                //LEFT MIDDLE RIGHT
                if (_attributes.PolojenieGorizontalno == 1) beam.Position.Plane = Position.PlaneEnum.LEFT;
                if (_attributes.PolojenieGorizontalno == 0) beam.Position.Plane = Position.PlaneEnum.MIDDLE;
                if (_attributes.PolojenieGorizontalno == 2) beam.Position.Plane = Position.PlaneEnum.RIGHT;

                // BACK BELOW FRONT TOP
                if (_attributes.PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.FRONT;
                if (_attributes.PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.TOP;

                // BEHIND  FRONT MIDLE
                if (_attributes.PolojenieVertikalno == 1) beam.Position.Depth = Position.DepthEnum.MIDDLE;
                if (_attributes.PolojenieVertikalno == 0) beam.Position.Depth = Position.DepthEnum.BEHIND;
                if (_attributes.PolojenieVertikalno == 2) beam.Position.Depth = Position.DepthEnum.FRONT;
            }

            beam.Modify();
        }

        private void Points()
        {
            int H = _yarusCount;

            ColumnStartPoint1 = new Point(0, 0 - Razdv_1_2, _basePoint.X * UklonbI[0] * 0.001);                           
            ColumnEndPoint1 = new Point(0, 0 - Razdv_1_2, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);     

            if (_count_column == 2)
            {
                ColumnStartPoint2 = new Point(0, 0 + Razdv_2_3, _basePoint.X * UklonbI[0] * 0.001);                                      
                ColumnEndPoint2 = new Point(0, 0 + Razdv_2_3, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);         
            }
            if (_count_column == 3)
            {
                 ColumnStartPoint2 = new Point(0, 0, _basePoint.X * UklonbI[0] * 0.001);                         
                 ColumnEndPoint2 = new Point(0, 0, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001); 

                if (Traversy2[H - 1] > Traversy[H - 1])
                    ColumnEndPoint2 = new Point(0, 0, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);

                ColumnStartPoint3 = new Point(0, 0 + Razdv_2_3, _basePoint.X * UklonbI[0] * 0.001);                                     
                ColumnEndPoint3 = new Point(0, 0 + Razdv_2_3, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);           
            }
        }
    }
}
