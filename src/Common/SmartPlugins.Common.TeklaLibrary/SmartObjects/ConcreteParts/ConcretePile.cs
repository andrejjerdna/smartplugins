using SmartPlugins.Common.TeklaLibrary.Geometry;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.SmartObjects
{
    public class ConcretePile : ConcretePartBase, IConcreteBase
    {
        public enum EdgeTypeEnum
        {
            YES,
            NO
        }
        public enum PileTypeEnum
        {
            TYPE1,
            TYPE2,
            TYPE3,
            TYPE4,
            TYPE5,
            TYPE6,
            TYPE7
        }
        public EdgeTypeEnum EdgeType { get; set; }
        public PileTypeEnum PileType { get; set; }

        public Point BasePoint;
        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }
        public double L4 { get; set; }
        public double L5 { get; set; }
        private List<ModelObject> _details = new List<ModelObject>();
        /// <summary>
        /// Длина сваи.
        /// </summary>
        private double _length;
        public ConcretePile(TSM.Model currentModel, Point basePoint, string profile, double length)
        {
            _currentModel = currentModel;
            BasePoint = basePoint;
            Profile = profile;
            _length = length;
            EdgeType = EdgeTypeEnum.NO;
            CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;
        }
        public void Insert()
        {
            InsertPile();
        }
        private Part InsertPile()
        {
            switch(PileType)
            {
                case PileTypeEnum.TYPE1:
                    return CreatePileType1();

                case PileTypeEnum.TYPE2:
                    return CreatePileType2();

                case PileTypeEnum.TYPE3:
                    return CreatePileType3();

                case PileTypeEnum.TYPE4:
                    return CreatePileType4();

                case PileTypeEnum.TYPE5:
                    return CreatePileType5();

                case PileTypeEnum.TYPE6:
                    return CreatePileType6();

                case PileTypeEnum.TYPE7:
                    return CreatePileType7();

                default:
                    return CreatePileType1();
            }
        }
        private Part CreatePileType1()
        {
            CastUnitType = Part.CastUnitTypeEnum.PRECAST;

            var point1 = new Point(0, 0, -_length);
            var point2 = new Point();

            var pile = new Beam(point1, point2);
            pile.Class = Class;
            pile.Material.MaterialString = Material;
            pile.Profile.ProfileString = GeneralMethods.GetPlatesProfile(L2, L1);
            pile.AssemblyNumber.Prefix = AssemblyPrefix;
            pile.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile.PartNumber.Prefix = PartPrefix;
            pile.PartNumber.StartNumber = PartStartNumber;
            pile.Name = PartName;
            pile.Finish = Finish;
            pile.PourPhase = PourPhase;
            pile.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile.Position.Depth = Position.DepthEnum.MIDDLE;
            pile.CastUnitType = CastUnitType;
            pile.Insert();

            _mainPart = pile;

            _details.Add(_mainPart);

            CreateEndType1();

            return pile;
        }
        private Part CreatePileType2()
        {
            var point1 = new Point(0, 0, -_length);
            var point2 = new Point();

            var pile = new Beam(point1, point2);
            pile.Class = Class;
            pile.Material.MaterialString = Material;
            pile.Profile.ProfileString = GeneralMethods.GetRoundProfile(L1);
            pile.AssemblyNumber.Prefix = AssemblyPrefix;
            pile.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile.PartNumber.Prefix = PartPrefix;
            pile.PartNumber.StartNumber = PartStartNumber;
            pile.Name = PartName;
            pile.Finish = Finish;
            pile.PourPhase = PourPhase;
            pile.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile.Position.Depth = Position.DepthEnum.MIDDLE;
            pile.CastUnitType = CastUnitType;
            pile.Insert();

            _mainPart = pile;

            _details.Add(_mainPart);

            return pile;
        }
        private Part CreatePileType3()
        {
            var point1 = new Point(0, 0, -_length);
            var point2 = new Point();

            var groutUnderBasePlate = new Beam(point1, point2);
            groutUnderBasePlate.Class = Class;
            groutUnderBasePlate.Material.MaterialString = Material;
            groutUnderBasePlate.Profile.ProfileString = GeneralMethods.GetConusProfile(L2, L1);
            groutUnderBasePlate.AssemblyNumber.Prefix = AssemblyPrefix;
            groutUnderBasePlate.AssemblyNumber.StartNumber = AssemblyStartNumber;
            groutUnderBasePlate.PartNumber.Prefix = PartPrefix;
            groutUnderBasePlate.PartNumber.StartNumber = PartStartNumber;
            groutUnderBasePlate.Name = PartName;
            groutUnderBasePlate.Finish = Finish;
            groutUnderBasePlate.PourPhase = PourPhase;
            groutUnderBasePlate.Position.Plane = Position.PlaneEnum.MIDDLE;
            groutUnderBasePlate.Position.Depth = Position.DepthEnum.MIDDLE;
            groutUnderBasePlate.CastUnitType = CastUnitType;
            groutUnderBasePlate.Insert();

            _mainPart = groutUnderBasePlate;

            _details.Add(_mainPart);

            return groutUnderBasePlate;
        }
        private Part CreatePileType4()
        {
            var point1 = new Point(0, 0, -_length + L2 - L2 / 2);
            var point2 = new Point();

            var pile1 = new Beam(point1, point2);
            pile1.Class = Class;
            pile1.Material.MaterialString = Material;
            pile1.Profile.ProfileString = GeneralMethods.GetRoundProfile(L1);
            pile1.AssemblyNumber.Prefix = AssemblyPrefix;
            pile1.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile1.PartNumber.Prefix = PartPrefix;
            pile1.PartNumber.StartNumber = PartStartNumber;
            pile1.Name = PartName;
            pile1.Finish = Finish;
            pile1.PourPhase = PourPhase;
            pile1.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile1.Position.Depth = Position.DepthEnum.MIDDLE;
            pile1.CastUnitType = CastUnitType;
            pile1.Insert();

            _mainPart = pile1;

            var point3 = new Point(0, 0, -_length + L2);
            var point4 = new Point(0, 0, -_length);

            var pile2 = new Beam(point4, point3);
            pile2.Class = Class;
            pile2.Material.MaterialString = Material;
            pile2.Profile.ProfileString = GeneralMethods.GetShereProfile(L2);
            pile2.AssemblyNumber.Prefix = AssemblyPrefix;
            pile2.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile2.PartNumber.Prefix = PartPrefix;
            pile2.PartNumber.StartNumber = PartStartNumber;
            pile2.Name = PartName;
            pile2.Finish = Finish;
            pile2.PourPhase = PourPhase;
            pile2.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile2.Position.Depth = Position.DepthEnum.MIDDLE;
            pile2.CastUnitType = CastUnitType;
            pile2.Insert();

            CreatePartBoolean(pile2);

            AddToAssembly(new List<Part> { pile2 });

            _details.Add(_mainPart);
            _details.Add(pile2);

            return pile1;
        }
        private Part CreatePileType5()
        {
            var point1 = new Point(0, 0, -_length + L3);
            var point2 = new Point();

            var pile1 = new Beam(point1, point2);
            pile1.Class = Class;
            pile1.Material.MaterialString = Material;
            pile1.Profile.ProfileString = GeneralMethods.GetRoundProfile(L1);
            pile1.AssemblyNumber.Prefix = AssemblyPrefix;
            pile1.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile1.PartNumber.Prefix = PartPrefix;
            pile1.PartNumber.StartNumber = PartStartNumber;
            pile1.Name = PartName;
            pile1.Finish = Finish;
            pile1.PourPhase = PourPhase;
            pile1.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile1.Position.Depth = Position.DepthEnum.MIDDLE;
            pile1.CastUnitType = CastUnitType;
            pile1.Insert();

            _mainPart = pile1;

            var point3 = new Point(0, 0, -_length + L3);
            var point4 = new Point(0, 0, -_length);

            var pile2 = new Beam(point4, point3);
            pile2.Class = Class;
            pile2.Material.MaterialString = Material;
            pile2.Profile.ProfileString = GeneralMethods.GetConusProfile(L2, L1);
            pile2.AssemblyNumber.Prefix = AssemblyPrefix;
            pile2.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile2.PartNumber.Prefix = PartPrefix;
            pile2.PartNumber.StartNumber = PartStartNumber;
            pile2.Name = PartName;
            pile2.Finish = Finish;
            pile2.PourPhase = PourPhase;
            pile2.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile2.Position.Depth = Position.DepthEnum.MIDDLE;
            pile2.CastUnitType = CastUnitType;
            pile2.Insert();

            AddToAssembly(new List<Part> { pile2 });

            _details.Add(_mainPart);
            _details.Add(pile2);

            return pile1;
        }
        private Part CreatePileType6()
        {
            var point1 = new Point(0, 0, -_length + L3 + L4);
            var point2 = new Point();

            var pile1 = new Beam(point1, point2);
            pile1.Class = Class;
            pile1.Material.MaterialString = Material;
            pile1.Profile.ProfileString = GeneralMethods.GetRoundProfile(L1);
            pile1.AssemblyNumber.Prefix = AssemblyPrefix;
            pile1.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile1.PartNumber.Prefix = PartPrefix;
            pile1.PartNumber.StartNumber = PartStartNumber;
            pile1.Name = PartName;
            pile1.Finish = Finish;
            pile1.PourPhase = PourPhase;
            pile1.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile1.Position.Depth = Position.DepthEnum.MIDDLE;
            pile1.CastUnitType = CastUnitType;
            pile1.Insert();

            _mainPart = pile1;

            var point3 = point1;
            var point4 = new Point(0, 0, -_length + L4);

            var pile2 = new Beam(point4, point3);
            pile2.Class = Class;
            pile2.Material.MaterialString = Material;
            pile2.Profile.ProfileString = GeneralMethods.GetConusProfile(L2, L1);
            pile2.AssemblyNumber.Prefix = AssemblyPrefix;
            pile2.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile2.PartNumber.Prefix = PartPrefix;
            pile2.PartNumber.StartNumber = PartStartNumber;
            pile2.Name = PartName;
            pile2.Finish = Finish;
            pile2.PourPhase = PourPhase;
            pile2.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile2.Position.Depth = Position.DepthEnum.MIDDLE;
            pile2.CastUnitType = CastUnitType;
            pile2.Insert();

            var point5 = new Point(0, 0, -_length + L4 + L4);
            var point6 = new Point(0, 0, -_length);

            var pile3 = new Beam(point6, point5);
            pile3.Class = Class;
            pile3.Material.MaterialString = Material;
            pile3.Profile.ProfileString = GeneralMethods.GetShereProfile(L2);
            pile3.AssemblyNumber.Prefix = AssemblyPrefix;
            pile3.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile3.PartNumber.Prefix = PartPrefix;
            pile3.PartNumber.StartNumber = PartStartNumber;
            pile3.Name = PartName;
            pile3.Finish = Finish;
            pile3.PourPhase = PourPhase;
            pile3.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile3.Position.Depth = Position.DepthEnum.MIDDLE;
            pile3.CastUnitType = CastUnitType;
            pile3.Insert();

            AddToAssembly(new List<Part> { pile2, pile3 });

            InsertCut(pile3, point4);

            _details.Add(_mainPart);
            _details.Add(pile2);
            _details.Add(pile3);

            return pile1;
        }
        private Part CreatePileType7()
        {
            var point1 = new Point(0, 0, -_length + L3 + L4);
            var point2 = new Point();

            var pile1 = new Beam(point1, point2);
            pile1.Class = Class;
            pile1.Material.MaterialString = Material;
            pile1.Profile.ProfileString = GeneralMethods.GetRoundProfile(L1);
            pile1.AssemblyNumber.Prefix = AssemblyPrefix;
            pile1.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile1.PartNumber.Prefix = PartPrefix;
            pile1.PartNumber.StartNumber = PartStartNumber;
            pile1.Name = PartName;
            pile1.Finish = Finish;
            pile1.PourPhase = PourPhase;
            pile1.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile1.Position.Depth = Position.DepthEnum.MIDDLE;
            pile1.CastUnitType = CastUnitType;
            pile1.Insert();

            _mainPart = pile1;

            var point3 = point1;
            var point4 = new Point(0, 0, -_length + L4);

            var pile2 = new Beam(point4, point3);
            pile2.Class = Class;
            pile2.Material.MaterialString = Material;
            pile2.Profile.ProfileString = GeneralMethods.GetConusProfile(L2, L1);
            pile2.AssemblyNumber.Prefix = AssemblyPrefix;
            pile2.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile2.PartNumber.Prefix = PartPrefix;
            pile2.PartNumber.StartNumber = PartStartNumber;
            pile2.Name = PartName;
            pile2.Finish = Finish;
            pile2.PourPhase = PourPhase;
            pile2.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile2.Position.Depth = Position.DepthEnum.MIDDLE;
            pile2.CastUnitType = CastUnitType;
            pile2.Insert();

            var point5 = point4;
            var point6 = new Point(0, 0, -_length);

            var pile3 = new Beam(point6, point5);
            pile3.Class = Class;
            pile3.Material.MaterialString = Material;
            pile3.Profile.ProfileString = GeneralMethods.GetConusProfile(1, L2);
            pile3.AssemblyNumber.Prefix = AssemblyPrefix;
            pile3.AssemblyNumber.StartNumber = AssemblyStartNumber;
            pile3.PartNumber.Prefix = PartPrefix;
            pile3.PartNumber.StartNumber = PartStartNumber;
            pile3.Name = PartName;
            pile3.Finish = Finish;
            pile3.PourPhase = PourPhase;
            pile3.Position.Plane = Position.PlaneEnum.MIDDLE;
            pile3.Position.Depth = Position.DepthEnum.MIDDLE;
            pile3.CastUnitType = CastUnitType;
            pile3.Insert();

            AddToAssembly(new List<Part> { pile2, pile3 });

            _details.Add(_mainPart);
            _details.Add(pile2);
            _details.Add(pile3);

            return pile1;
        }
        private void CreateEndType1()
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var workCS = _mainPart.GetCoordinateSystem();
            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(workCS));
            workCS = new CoordinateSystem(new Point(), new Vector(0, 0, -1), new Vector(0, 1, 0));
            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(workCS));

            var mainPartGeometry = new ModelObjectGeometry(_currentModel, _mainPart);

            var listPoints = new List<Point>
            {
                new Point(mainPartGeometry.MaxX, mainPartGeometry.MaxY, 0),
                new Point(mainPartGeometry.MaxX, mainPartGeometry.MinY, 0),
                new Point(mainPartGeometry.MinX, mainPartGeometry.MinY, 0),
                new Point(mainPartGeometry.MinX, mainPartGeometry.MaxY, 0),
                new Point(mainPartGeometry.MaxX, mainPartGeometry.MaxY, 0)
            };

            for (int i = 0; i < listPoints.Count() - 1; i++)
            {
                var edge = new EdgeChamfer(listPoints[i], listPoints[i + 1]);
                edge.Father = _mainPart;
                edge.FirstChamferEndType = EdgeChamfer.ChamferEndTypeEnum.FULL;
                edge.Chamfer.X = L3;
                edge.Chamfer.Y = L4 -0.01;
                edge.Insert();
            }

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
        private void CreatePartBoolean(Beam pile2)
        {
            var classPile = pile2.Class;
            pile2.Class = BooleanPart.BooleanOperativeClassName;
            pile2.Modify();

            var booleanPart = new BooleanPart();
            booleanPart.Father = _mainPart;
            booleanPart.SetOperativePart(pile2);
            booleanPart.Insert();

            pile2.Class = classPile;
            pile2.Modify();
        }
        private void AddToAssembly(List<Part> details)
        {
            var assembly = _mainPart.GetAssembly();

            foreach(Part part in details)
            {
                assembly.Add(part);
            }

            assembly.Modify();
        }
        private void InsertCut(Beam beam, Point point)
        {
            var cut = new CutPlane();
            cut.Plane.Origin = point;
            cut.Plane.AxisX = new Vector(1, 0, 0);
            cut.Plane.AxisY = new Vector(0, 1, 0);
            cut.Father = beam;
            cut.Insert();

        }
        /// <summary>
        /// Метод возвращает все детали сваи. Первая деталь - главная.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModelObject> GetDetails()
        {
            return _details;
        }
    }
}
