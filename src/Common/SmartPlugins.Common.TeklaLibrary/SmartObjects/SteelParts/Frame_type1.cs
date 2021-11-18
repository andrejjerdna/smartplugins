using SmartPlugins.Common.TeklaLibrary.CSLib;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.SmartObjects.SteelParts
{
    public class Frame_type1
    {
        private Model _model;

        private Point _point1;
        private Point _point2;

        public string ColumnProfile;
        private string _beam1Profile { get => ColumnProfile; }

        public IEnumerable<double> DistanceListY;
        public IEnumerable<double> DistanceListZ;
        public IEnumerable<double> DistanceListX;

        private List<List<Point>> _pointsList1;
        private List<List<Point>> _pointsList2;

        public Frame_type1(Model model, Point point1, Point point2)
        {
            _model = model;

            _point1 = point1;
            _point2 = point2;

            DistanceListY = new List<double> { 6000 };
            DistanceListZ = new List<double> { 6000 };
            DistanceListX = new List<double> { 0 };

            _pointsList1 = new List<List<Point>>();
            _pointsList2 = new List<List<Point>>();
        }

        public bool Insert()
        {
            var currentTP = _model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var workTP = GetTransformationPlane();
            _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(workTP);

            DrawUI.DrawPlane();

            var spacing = 0.0;

            foreach (double x in DistanceListX)
            {
                spacing += x;
                InsertFrame(spacing);
            }

            InsertBeams();

            _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

            return true;
        }

        private TransformationPlane GetTransformationPlane()
        {
            var currentTP = _model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var baseTP = new TransformationPlane();

            _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

            _point1 = baseTP.TransformationMatrixToLocal.Transform(currentTP.TransformationMatrixToGlobal.Transform(_point1));
            _point2 = baseTP.TransformationMatrixToLocal.Transform(currentTP.TransformationMatrixToGlobal.Transform(_point2));

            var workCS = new CoordinateSystem
            {
                Origin = _point1,
                AxisX = new Vector(_point2 - _point1),
                AxisY = new Vector(0, 0, 1)
            };

            var workPlane = new TransformationPlane(workCS);
            _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(workPlane);

            workCS = new CoordinateSystem
            {
                Origin = new Point(),
                AxisX = new Vector(1, 0, 0),
                AxisY = new Vector(0, 0, -1)
            };

            workPlane = new TransformationPlane(workCS);

            _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

            return workPlane;
        }

        private void InsertFrame(double x)
        {
            var y = DistanceListY.First() / 2;

            var z = DistanceListZ.Last();

            var p1 = new Point(x, y, 0);
            var p2 = new Point(x, y, z);

            var p3 = new Point(x, -y, 0);
            var p4 = new Point(x, -y, z);

            var beam1 = new Beam(p1, p2);
            beam1.Profile.ProfileString = ColumnProfile;
            beam1.Class = "5";
            beam1.Position.Depth = Position.DepthEnum.MIDDLE;
            beam1.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam1.Position.RotationOffset = 90;
            beam1.Insert();

            var beam2 = new Beam(p3, p4);
            beam2.Profile.ProfileString = ColumnProfile;
            beam2.Class = "5";
            beam2.Position.Depth = Position.DepthEnum.MIDDLE;
            beam2.Position.Plane = Position.PlaneEnum.MIDDLE;
            beam2.Position.RotationOffset = 90;
            beam2.Insert();

            var listPoinrs1 = new List<Point>();
            var listPoinrs2 = new List<Point>();

            foreach (double beamZ in DistanceListZ)
            {
                var p5 = new Point(x, p1.Y, beamZ);
                var p6 = new Point(x, p3.Y, beamZ);

                var beam3 = new Beam(p5, p6);
                beam3.Profile.ProfileString = _beam1Profile;
                beam3.Class = "6";
                beam3.Position.Depth = Position.DepthEnum.BEHIND;
                beam3.Position.Plane = Position.PlaneEnum.MIDDLE;
                //beam3.Position.RotationOffset = 90;
                beam3.Insert();

                listPoinrs1.Add(p5);
                listPoinrs2.Add(p6);
            }

            _pointsList1.Add(listPoinrs1);
            _pointsList2.Add(listPoinrs2);
        }

        private void InsertBeams()
        {
            for (int i = 0; i < _pointsList1.Count - 1; i++)
            {
                var points1 = _pointsList1[i];
                var points2 = _pointsList1[i + 1];

                var points3 = _pointsList2[i];
                var points4 = _pointsList2[i + 1];

                for (int j = 0; j < points1.Count; j++)
                {
                    var p1 = points1[j];
                    var p2 = points2[j];

                    var beam1 = new Beam(p1, p2);
                    beam1.Profile.ProfileString = _beam1Profile;
                    beam1.Class = "9";
                    beam1.Position.Depth = Position.DepthEnum.BEHIND;
                    beam1.Position.Plane = Position.PlaneEnum.MIDDLE;
                    //beam3.Position.RotationOffset = 90;
                    beam1.Insert();

                    var p3 = points3[j];
                    var p4 = points4[j];

                    var beam2 = new Beam(p3, p4);
                    beam2.Profile.ProfileString = _beam1Profile;
                    beam2.Class = "9";
                    beam2.Position.Depth = Position.DepthEnum.BEHIND;
                    beam2.Position.Plane = Position.PlaneEnum.MIDDLE;
                    //beam3.Position.RotationOffset = 90;
                    beam2.Insert();
                }
            }
        }
    }
}
