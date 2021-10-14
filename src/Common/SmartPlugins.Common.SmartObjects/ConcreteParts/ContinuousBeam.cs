using SmartPlugins.Common.CSLib;
using SmartPlugins.Common.SmartGeometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartObjects
{
    public class ContinuousBeam : ConcretePartBase, IConcreteBase
    {
        public enum ContinuousBeamTypeEnum
        {
            BETWEENCOLUMS,
            OVERCOLUMS
        }
        public ContinuousBeamTypeEnum ContinuousBeamType { get; set; }
        public int Priority { get; set; }
        private Part _startColumn;
        private Part _endColumn;
        private Part _plate;
        private IEnumerable<Part> _allColumn;
        private List<Part> _beams;
        private List<Point> _listPoint1;
        private List<Point> _listPoint2;
        private Assembly _assembly;
        private const string _markParam = "USER_FIELD_1";
        private const string _statusParam = "USER_FIELD_2";
        private IEnumerable<Part> _beamsAssembly;

        private ModelObjectGeometry _startColumnGeometry;
        private ModelObjectGeometry _endColumnGeometry;

        public double L1;
        public double L2;

        public ContinuousBeam(tsm.Model currentModel, IEnumerable<Part> allColums, Part plate)
        {
            _currentModel = currentModel;
            _plate = plate;
            _allColumn = allColums;
            _beams = new List<Part>();
            _listPoint1 = new List<Point>();
            _listPoint2 = new List<Point>();
            _assembly = _plate.GetAssembly();
            _beamsAssembly = GetBeamsAssembly();

            Priority = 0;
            ContinuousBeamType = ContinuousBeamTypeEnum.BETWEENCOLUMS;
            L1 = 400.0;
            L2 = 400.0;
        }

        public void Insert()
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            if (_allColumn.Count() < 2)
                return;

            Profile = GetProfile();

            _startColumn = _allColumn.First();
            _endColumn = _allColumn.Last();

            var localTP = GetTransformationPlane();

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(localTP);

            GetFittingColumn();

             if (ContinuousBeamType == ContinuousBeamTypeEnum.BETWEENCOLUMS)
            InsertBeam1();

            if (ContinuousBeamType == ContinuousBeamTypeEnum.OVERCOLUMS)
                InsertBeam2();

            GetFittingBeams();

            AddBeamsToAssembly();

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
        private string GetProfile()
        {
            return L1.ToString() + "*" + L2.ToString();
        }
        private void GetFittingColumn()
        {
            var originPoint = new Point(0, 0, 0);

            if(ContinuousBeamType == ContinuousBeamTypeEnum.OVERCOLUMS)
            {
                originPoint = new Point(0, -L2, 0);
            }

            foreach (Part part in _allColumn)
            {
                var fitting = new Fitting
                {
                    Father = part,
                    Plane = new Plane
                    {
                        Origin = originPoint,
                        AxisX = new Vector(1, 0, 0),
                        AxisY = new Vector(0, 0, 1)
                    }
                };
                fitting.Insert();
            }
        }
        private void InsertBeam1()
        {
            GetX();

            _listPoint1.RemoveAt(_listPoint1.Count - 1);
            _listPoint1.RemoveAt(0);

            for (int i = 0; i < _listPoint1.Count; i += 2)
            {
                var point1 = new Point(_listPoint1[i].X, 0, 0);
                var point2 = new Point(_listPoint1[i + 1].X, 0, 0);

                var beam = new Beam(point1, point2);
                beam.Class = Class;
                beam.Material.MaterialString = Material;
                beam.Profile.ProfileString = Profile;
                beam.AssemblyNumber.Prefix = AssemblyPrefix;
                beam.AssemblyNumber.StartNumber = AssemblyStartNumber;
                beam.PartNumber.Prefix = PartPrefix;
                beam.PartNumber.StartNumber = PartStartNumber;
                beam.Name = PartName;
                beam.Finish = Finish;
                beam.PourPhase = PourPhase;
                beam.Position.Plane = Position.PlaneEnum.RIGHT;
                beam.Position.Depth = Position.DepthEnum.MIDDLE;
                beam.CastUnitType = CastUnitType;

                if (beam.Insert())
                {
                    _beams.Add(beam);
                    WriteMark(beam);
                    WritePriority(beam);
                }
            }
        }
        private void InsertBeam2()
        {
            GetX();

            var point1 = new Point(_listPoint1.First().X, 0, 0);
            var point2 = new Point(_listPoint1.Last().X, 0, 0);

            var beam = new Beam(point1, point2);
            beam.Class = Class;
            beam.Material.MaterialString = Material;
            beam.Profile.ProfileString = Profile;
            beam.AssemblyNumber.Prefix = AssemblyPrefix;
            beam.AssemblyNumber.StartNumber = AssemblyStartNumber;
            beam.PartNumber.Prefix = PartPrefix;
            beam.PartNumber.StartNumber = PartStartNumber;
            beam.Name = PartName;
            beam.Finish = Finish;
            beam.PourPhase = PourPhase;
            beam.Position.Plane = Position.PlaneEnum.RIGHT;
            beam.Position.Depth = Position.DepthEnum.MIDDLE;
            beam.CastUnitType = CastUnitType;

            if (beam.Insert())
            {
                _beams.Add(beam);
                WriteMark(beam);
                WritePriority(beam);
                GetBooleanParts();
            }
        }
        private void WriteMark(Part beam)
        {
            beam.SetUserProperty(_markParam, nameof(ContinuousBeam));
            beam.Modify();
        }
        private void WritePriority(Part beam)
        {
            beam.SetUserProperty(_statusParam, Priority.ToString());
            beam.Modify();
        }
        private int GetPriority(Part beam)
        {
            var result = 0;
            var priority = "0";

            beam.GetReportProperty(_statusParam, ref priority);

            if (int.TryParse(priority, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        private void GetX()
        {
            var listX1 = new List<Point>();
            var listX2 = new List<Point>();

            _startColumnGeometry = new ModelObjectGeometry(_currentModel, _startColumn);
            _endColumnGeometry = new ModelObjectGeometry(_currentModel, _endColumn);

            var y = Math.Min(_startColumnGeometry.MaxY, _endColumnGeometry.MaxY);

            var line1 = new LineSegment(new Point(_startColumnGeometry.MinX - 10000, y, 0), new Point(_endColumnGeometry.MaxX + 10000, y, 0));
            var line2 = new LineSegment(new Point(_startColumnGeometry.MinX - 10000, y - 1, 0), new Point(_endColumnGeometry.MaxX + 10000, y - 1, 0));

            foreach (Part column in _allColumn)
            {
                var columnGeometry = new ModelObjectGeometry(_currentModel, column);
                var intersect = columnGeometry.GetSolid().Intersect(line1).OfType<Point>();
                listX1.AddRange(intersect);
                var intersect2 = columnGeometry.GetSolid().Intersect(line2).OfType<Point>();
                listX2.AddRange(intersect2);
            }

            _listPoint1 = listX1.OrderBy(point => point.X).ToList();
            _listPoint2 = listX2.OrderBy(point => point.X).ToList();
        }
        private void GetFittingBeams()
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var countX = 0;
            return;

            foreach (Beam beam in _beams)
            {
                var index = _beams.IndexOf(beam);

                var x1 = _listPoint1.ElementAt(countX);
                var x2 = _listPoint2.ElementAt(countX);

                var cs = new CoordinateSystem(new Point(), new Vector(x1 - x2), new Vector(0, 0, 1));
                var workTP = new TransformationPlane(cs);

                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(workTP);

                DrawUI.DrawPlane();

                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
                countX += 2;
            }

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
        private TransformationPlane GetTransformationPlane()
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var startColumsCS = _startColumn.GetCoordinateSystem();
            var startColumsTP = new TransformationPlane(startColumsCS);
            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(startColumsTP);

            var startColumnGeometry = new ModelObjectGeometry(_currentModel, _startColumn);
            var endColumnGeometry = new ModelObjectGeometry(_currentModel, _endColumn);

            var solidPlate = _plate.GetSolid();

            var intersectLine1 = new LineSegment(new Point(startColumnGeometry.MaxX - 10000, 0, 0), new Point(startColumnGeometry.MaxX + 10000, 0, 0));
            var intersectLine2 = new LineSegment(new Point(endColumnGeometry.MaxX - 10000, endColumnGeometry.MidY, endColumnGeometry.MidZ), new Point(endColumnGeometry.MaxX + 10000, endColumnGeometry.MidY, endColumnGeometry.MidZ));

            var intersect1 = solidPlate.Intersect(intersectLine1).OfType<Point>().OrderBy(point => point.X);
            var intersect2 = solidPlate.Intersect(intersectLine2).OfType<Point>().OrderBy(point => point.X);

            var basePoint = intersect1.First();
            var p1 = intersect1.First();
            var p2 = intersect2.First();

            var resultCS = new CoordinateSystem(basePoint, new Vector(p2 - p1), new Vector(1, 0, 0));
            var resultTP = new TransformationPlane(resultCS);

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

            return resultTP;
        }
        private void AddBeamsToAssembly()
        {
            foreach (Part beam in _beams)
            {
                _assembly.Add(beam);
            }

            _assembly.Modify();
        }
        private IEnumerable<Part> GetBeamsAssembly()
        {
            var continuousBeams = new List<Part>();

            var beams = _assembly.GetSecondaries();

           // MessageBox.Show(beams.Count.ToString());
            /*
            foreach (Part part in beams)
            {
                var mark = "";
                part.GetReportProperty(_markParam, ref mark);

                if (mark == nameof(ContinuousBeam))
                {
                    continuousBeams.Add(part);
                }
            }
            */
            return continuousBeams;
        }
        private void GetBooleanParts()
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            foreach (Part beam in _beams)
            {
                var beamCS = beam.GetCoordinateSystem();
                var beamTP = new TransformationPlane(beamCS);
                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(beamTP);

                var pointCenterLine = beam.GetCenterLine(false).OfType<Point>();

                var line = new LineSegment(pointCenterLine.First(), pointCenterLine.Last());

                foreach (Part beamAssembly in _beamsAssembly)
                {
                    CreatePartBoolean(beamAssembly, beam, line);
                }

                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            }
            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
        /// <summary>
        /// Создаем тело выреза.
        /// </summary>
        /// <param name="blindingOBB"></param>
        /// <param name="beam"></param>
        private void CreatePartBoolean(Part beamAssembly, Part beam, LineSegment line)
        {
           
            var priorityBeam1 = GetPriority(beam);
            var priorityBeam2 = GetPriority(beamAssembly);

            var solidBeamAssembly = beamAssembly.GetSolid();

            var intersect = solidBeamAssembly.Intersect(line).OfType<Point>().OrderBy(point => point.X);

            if (intersect.Count() == 0) return;

            var beamSolid = beam.GetSolid();

            var p1 = new Point(intersect.First().X - 50, 0, 0);
            var p2 = new Point(intersect.Last().X + 50, 0, 0);

            //Вырезаем тело сваи из бетонной подготовки.
            var partBoolean = new Beam(p1, p2);
            partBoolean.Profile.ProfileString = beam.Profile.ProfileString;
            partBoolean.Material.MaterialString = beam.Material.MaterialString;
            partBoolean.Position = beam.Position;
            partBoolean.PartNumber.Prefix = "B1";
            partBoolean.Class = BooleanPart.BooleanOperativeClassName;
            partBoolean.Insert();

            var booleanPart = new BooleanPart();
            booleanPart.Father = _mainPart;
            booleanPart.SetOperativePart(beam);
            booleanPart.Insert();

            partBoolean.Delete();
        }

    }
}
