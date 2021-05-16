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
        public TransformationPlane workTP;

        public Point _basePoint;

        public List<Beam> _Columns = new List<Beam>();
        public List<Beam> _Travers = new List<Beam>();
        public List<Beam> _TraversRight = new List<Beam>();
        public List<Beam> _TraversLeft = new List<Beam>();
        public List<double> Z { get; set; }

        public Frame(Model M, Point basePoint, int yarusCount, int count_column)
        {
            _basePoint = basePoint;
            _yarusCount = yarusCount;
            _count_column = count_column;
            _M = M;
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
            if (_count_column == 2)
            {
                CreateRamaDveKolony();
                Con.BeamsToColumn(_Columns[0], _TraversRight);
                Con.BeamsToColumn(_Columns[1], _TraversRight);
            }
            if (_count_column == 3)
            {
                CreateRamaTriKolony();
                Con.BeamsToColumn(_Columns[0], _TraversRight);
                Con.BeamsToColumn(_Columns[1], _TraversRight);
                Con.BeamsToColumn(_Columns[1], _TraversLeft);
                Con.BeamsToColumn(_Columns[2], _TraversLeft);
            } 
            _M.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }

        private void CreateRamaDveKolony()
        {
            int H = _yarusCount;

            Point C_Start_point1 = new Point(0 , 0 - Razdv_1_2, 0 + _basePoint.X * UklonbI[0] * 0.001 );                           //точка низа 1 колонны
            Point C_End_point1 = new Point(0 , 0 - Razdv_1_2, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1]* 0.001);     //точка верха  1 колонны 

            Point C_Start_point2 = new Point(0 , 0 + Razdv_2_3, 0 + _basePoint.X * UklonbI[0] * 0.001);                           //точка низа 2 колонны
            Point C_End_point2 = new Point(0 , 0 + Razdv_2_3, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1]* 0.001);     //точка верха  2 колонны 
 
            _Columns.Add(Beam_main(AttributeColumn[0], C_Start_point1, C_End_point1));
            _Columns.Add(Beam_main(AttributeColumn[1], C_Start_point2, C_End_point2));

            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_1_start = new Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count]+ _basePoint.X * UklonbI[_count]* 0.001);
                Point B_1_end = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count]+ _basePoint.X * UklonbI[_count]* 0.001);
                _TraversRight.Add(Beam_main(Attributes[_count], B_1_start, B_1_end));
                _Travers.Add(_TraversRight[_count]);
            }
        }

        private void CreateRamaTriKolony()
           {
               int H = _yarusCount;

            

               Point C_Start_point1 = new Point(0, 0 - Razdv_1_2, _basePoint.X * UklonbI[0] * 0.001);                           //точка низа 1 колонны
               Point C_End_point1 = new Point(0, 0 - Razdv_1_2, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);     //точка верха  1 колонны 

               Point C_Start_point2 = new Point(0, 0 , _basePoint.X * UklonbI[0] * 0.001);                           //точка низа 2 колонны
               Point C_End_point2 = new Point(0, 0, Traversy[H - 1] + 100 + _basePoint.X * UklonbI[H - 1]* 0.001);  //точка верха  2 колонны 

                if (Traversy2[H-1] > Traversy[H-1])
                {
                   C_End_point2 = new Point(0, 0, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);
                }


               Point C_Start_point3 = new Point(0, 0 + Razdv_2_3, _basePoint.X * UklonbI[0] * 0.001);                                       //точка низа 3 колонны
               Point C_End_point3 = new Point(0, 0 + Razdv_2_3, Traversy2[H - 1] + 100 + _basePoint.X * UklonbI[H - 1] * 0.001);            //точка верха  3 колонны 

            _Columns.Add(Beam_main(AttributeColumn[0], C_Start_point1, C_End_point1));
            _Columns.Add(Beam_main(AttributeColumn[1], C_Start_point2, C_End_point2));
            _Columns.Add(Beam_main(AttributeColumn[2],C_Start_point3, C_End_point3));

            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_1_start = new Point(C_Start_point1.X, C_Start_point1.Y, Traversy[_count] + _basePoint.X * UklonbI[_count]*0.001);
                Point B_1_end = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy[_count] + _basePoint.X * UklonbI[_count]*0.001);
                _TraversRight.Add(Beam_main(Attributes[_count], B_1_start, B_1_end));
                _Travers.Add(_TraversRight[_count]);
            }

            for (int _count = 0; _count < _yarusCount; _count++)
            {
                Point B_2_start = new Point(C_Start_point2.X, C_Start_point2.Y, Traversy2[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                Point B_2_end = new Point(C_Start_point3.X, C_Start_point3.Y, Traversy2[_count] + _basePoint.X * UklonbI[_count] * 0.001);
                _TraversLeft.Add(Beam_main(Attributes2[_count], B_2_start, B_2_end));
                _Travers.Add(_TraversRight[_count]);
            }
        }

 

        public Beam Beam_main(Attributes attributes, Point startPoint, Point endPoint)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";

            if (newBeam.Insert())
            {
                SetAtt(newBeam, attributes);
            }
            return newBeam;
        }
        public Beam Beam_main(Point startPoint, Point endPoint)
        {
            Beam newBeam = new Beam(startPoint, endPoint);
            newBeam.Profile.ProfileString = "I30K1_20_93";
            newBeam.Insert();
            return newBeam;
        }

        private void SetAtt(Beam beam, Attributes _attributes)
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
               // if (_attributes.PolojeniePovorot == 0) beam.Position.Rotation = Position.RotationEnum.BACK;
               // if (_attributes.PolojeniePovorot == 1) beam.Position.Rotation = Position.RotationEnum.BELOW;

                // BEHIND  FRONT MIDLE
                if (_attributes.PolojenieVertikalno == 1) beam.Position.Depth = Position.DepthEnum.MIDDLE;
                if (_attributes.PolojenieVertikalno == 0) beam.Position.Depth = Position.DepthEnum.BEHIND;
                if (_attributes.PolojenieVertikalno == 2) beam.Position.Depth = Position.DepthEnum.FRONT;

            }

            beam.Modify(); 
        }


    }
}
