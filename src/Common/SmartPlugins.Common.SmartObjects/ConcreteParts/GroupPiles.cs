using SmartPlugins.Common.SmartTeklaModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartObjects
{
    public class GroupPiles : ConcretePartBase
    {
        public enum GroupTypeEnum
        {
            ONE_PILE,
            RECTANGLE,
            CIRCLE,
            XY_LIST
        }
        /// <summary>
        /// Длина сваи.
        /// </summary>
        private double Length;
        /// <summary>
        /// Смещение точки вставки сваи от базовой точки.
        /// </summary>
        private double Delta;
        public GroupTypeEnum GroupType { get; set; }
        public ConcretePile.PileTypeEnum PileType { get; set; }
        private int EdgeTypeUser;
        private IEnumerable<Point> BasePoint;
        private IEnumerable<double> StepOnAxisX;
        private IEnumerable<double> StepOnAxisY;
        public string StepsX;
        public string StepsY;
        public IEnumerable<Tuple<string, string>> Attibutes;

        public double L1 { get; set; }
        public double L2 { get; set; }
        public double L3 { get; set; }
        public double L4 { get; set; }
        public double L5 { get; set; }

        public GroupPiles(TSM.Model currentModel, IEnumerable<Point> basePoints, int edgeTypeUser, string profile, double length, double delta)
        {
            _currentModel = currentModel;
            BasePoint = basePoints;
            EdgeTypeUser = edgeTypeUser;
            Profile = profile;
            Length = length;
            Delta = delta;
        }
        public void Insert()
        {
            var points = GetPoints();

            InsertGroup(points);
        }

        private void InsertGroup(IEnumerable<Point> points)
        {
            var currentTP = _currentModel.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            foreach (Point point in points)
            {
                var workCS = new CoordinateSystem(point, new Vector(1, 0, 0), new Vector(0, 1, 0));
                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(workCS));

                var pile = new ConcretePile(_currentModel, point, Profile, Length)
                {
                    Class = Class,
                    Material = Material,
                    AssemblyPrefix = AssemblyPrefix,
                    PartPrefix = PartPrefix,
                    AssemblyStartNumber = AssemblyStartNumber,
                    PartStartNumber = PartStartNumber,
                    AssemblyName = AssemblyName,
                    PartName = PartName,
                    Finish = Finish,
                    PourPhase = PourPhase,
                    L1 = L1,
                    L2 = L2,
                    L3 = L3,
                    L4 = L4,
                    L5 = L5
                };

                pile.PileType = PileType;

                pile.Insert();

                //Задаем дополнительные аттрибуты.
                foreach(Tuple<string, string> attr in Attibutes)
                {
                    GeneralMethods.SerUserAttribute(pile.GetDetails(), attr.Item1, attr.Item2);
                }

                _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
            }

            _currentModel.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
        private IEnumerable<Point> GetPoints()
        {
            StepOnAxisX = BoltsMethods.GetDistanceList(StepsX);
            StepOnAxisY = BoltsMethods.GetDistanceList(StepsY);

            switch(GroupType)
            {
                case GroupTypeEnum.ONE_PILE:
                    return new List<Point> { new Point(BasePoint.First().X, BasePoint.First().Y, Delta)};

                case GroupTypeEnum.RECTANGLE:
                    return GetRectanglePoints();

                case GroupTypeEnum.CIRCLE:
                    return GetCirclePoints();

                case GroupTypeEnum.XY_LIST:
                    return GetXYListPoints();

                default:
                    return new List<Point>();
            }
        }
        private List<Point> GetRectanglePoints()
        {
            var tempBolt = new BoltArray();
            var tempContourPlate = new ContourPlate();

            try
            {
                tempContourPlate.Profile.ProfileString = "PL10";
                tempContourPlate.Material.MaterialString = "Concrete_Undefined";
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, -10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, -10000, 0), null));

                tempBolt.StartPointOffset.Dx = StepOnAxisX.Sum() / 2;

                if (!tempContourPlate.Insert())
                    return new List<Point>();

                tempBolt.PartToBeBolted = tempContourPlate;
                tempBolt.PartToBoltTo = tempContourPlate;

                tempBolt.FirstPosition = BasePoint.First();
                tempBolt.SecondPosition = new Point(BasePoint.First().X+100, BasePoint.First().Y, 0);

                tempBolt.BoltSize = 16;
                tempBolt.Tolerance = 3.00;
                tempBolt.BoltStandard = "7968";
                tempBolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                tempBolt.CutLength = 105;

                tempBolt.Length = 50;
                tempBolt.ExtraLength = 15;
                tempBolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                tempBolt.Position.Depth = Position.DepthEnum.MIDDLE;
                tempBolt.Position.Plane = Position.PlaneEnum.MIDDLE;
                tempBolt.Position.Rotation = Position.RotationEnum.FRONT;

                tempBolt.Bolt = true;
                tempBolt.Washer1 = true;
                tempBolt.Washer2 = true;
                tempBolt.Washer3 = true;
                tempBolt.Nut1 = true;
                tempBolt.Nut2 = true;

                tempBolt.Hole1 = true;
                tempBolt.Hole2 = true;
                tempBolt.Hole3 = true;
                tempBolt.Hole4 = true;
                tempBolt.Hole5 = true;

                foreach (double x in StepOnAxisX)
                {
                    tempBolt.AddBoltDistX(x);
                }

                foreach (double y in StepOnAxisY)
                {
                    tempBolt.AddBoltDistY(y);
                }

                if (!tempBolt.Insert())
                    return new List<Point>();

                return GetPointList(tempBolt);
            }
            catch(Exception ex)
            {
                return new List<Point>();
            }
            finally
            {
                if(tempBolt != null)
                tempBolt.Delete();

                if(tempContourPlate != null)
                tempContourPlate.Delete();
            }
        }
        private List<Point> GetXYListPoints()
        {
            var tempBolt = new BoltXYList();
            var tempContourPlate = new ContourPlate();

            try
            {
                tempContourPlate.Profile.ProfileString = "PL10";
                tempContourPlate.Material.MaterialString = "Concrete_Undefined";
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, -10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, -10000, 0), null));

                if (!tempContourPlate.Insert())
                    return new List<Point>();

                tempBolt.PartToBeBolted = tempContourPlate;
                tempBolt.PartToBoltTo = tempContourPlate;

                tempBolt.FirstPosition = BasePoint.First();
                tempBolt.SecondPosition = new Point(BasePoint.First().X + 100, BasePoint.First().Y, 0);

                tempBolt.BoltSize = 16;
                tempBolt.Tolerance = 3.00;
                tempBolt.BoltStandard = "7968";
                tempBolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                tempBolt.CutLength = 105;

                tempBolt.Length = 50;
                tempBolt.ExtraLength = 15;
                tempBolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                tempBolt.Position.Depth = Position.DepthEnum.MIDDLE;
                tempBolt.Position.Plane = Position.PlaneEnum.MIDDLE;
                tempBolt.Position.Rotation = Position.RotationEnum.FRONT;

                tempBolt.Bolt = true;
                tempBolt.Washer1 = true;
                tempBolt.Washer2 = true;
                tempBolt.Washer3 = true;
                tempBolt.Nut1 = true;
                tempBolt.Nut2 = true;

                tempBolt.Hole1 = true;
                tempBolt.Hole2 = true;
                tempBolt.Hole3 = true;
                tempBolt.Hole4 = true;
                tempBolt.Hole5 = true;

                foreach (double x in StepOnAxisX)
                {
                    tempBolt.AddBoltDistX(x);
                }

                foreach (double y in StepOnAxisY)
                {
                    tempBolt.AddBoltDistY(y);
                }

                if (!tempBolt.Insert())
                    return new List<Point>();

                return GetPointList(tempBolt);
            }
            catch (Exception ex)
            {
                return new List<Point>();
            }
            finally
            {
                if (tempBolt != null)
                    tempBolt.Delete();

                if (tempContourPlate != null)
                    tempContourPlate.Delete();
            }
        }
        private List<Point> GetCirclePoints()
        {
            var tempBolt = new BoltCircle();
            var tempContourPlate = new ContourPlate();

            try
            {
                tempContourPlate.Profile.ProfileString = "PL10";
                tempContourPlate.Material.MaterialString = "Concrete_Undefined";
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, -10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(-10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, 10000, 0), null));
                tempContourPlate.AddContourPoint(new ContourPoint(new Point(10000, -10000, 0), null));

                if (!tempContourPlate.Insert())
                    return new List<Point>();

                tempBolt.PartToBeBolted = tempContourPlate;
                tempBolt.PartToBoltTo = tempContourPlate;

                tempBolt.FirstPosition = BasePoint.First();
                tempBolt.SecondPosition = new Point(BasePoint.First().X + 100, BasePoint.First().Y, 0);

                tempBolt.BoltSize = 16;
                tempBolt.Tolerance = 3.00;
                tempBolt.BoltStandard = "7968";
                tempBolt.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
                tempBolt.CutLength = 105;

                tempBolt.Length = 50;
                tempBolt.ExtraLength = 15;
                tempBolt.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

                tempBolt.Position.Depth = Position.DepthEnum.MIDDLE;
                tempBolt.Position.Plane = Position.PlaneEnum.MIDDLE;
                tempBolt.Position.Rotation = Position.RotationEnum.FRONT;

                tempBolt.Bolt = true;
                tempBolt.Washer1 = true;
                tempBolt.Washer2 = true;
                tempBolt.Washer3 = true;
                tempBolt.Nut1 = true;
                tempBolt.Nut2 = true;

                tempBolt.Hole1 = true;
                tempBolt.Hole2 = true;
                tempBolt.Hole3 = true;
                tempBolt.Hole4 = true;
                tempBolt.Hole5 = true;

                tempBolt.NumberOfBolts = StepOnAxisX.First();
                tempBolt.Diameter = StepOnAxisY.First();

                if (!tempBolt.Insert())
                    return new List<Point>();

                return GetPointList(tempBolt);
            }
            catch (Exception ex)
            {
                return new List<Point>();
            }
            finally
            {
                if (tempBolt != null)
                    tempBolt.Delete();

                if (tempContourPlate != null)
                    tempContourPlate.Delete();
            }
        }

        /// <summary>
        /// Получаем точки временной группы болтов.
        /// </summary>
        /// <param name="bg">Группа болтов.</param>
        /// <returns></returns>
        private List<Point> GetPointList(BoltGroup bg)
        {
            var result = new List<Point>();

            bg.Select();

            foreach (Point boltPosition in bg.BoltPositions)
            {
                result.Add(new Point(boltPosition.X, boltPosition.Y, 0));
            }

            return result;
        }
    }
}
