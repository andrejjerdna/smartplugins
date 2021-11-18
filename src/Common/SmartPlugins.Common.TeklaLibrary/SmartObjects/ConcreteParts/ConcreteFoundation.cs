using SmartPlugins.Common.TeklaLibrary.Geometry;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.SmartObjects
{
    public class ConcreteFoundation : ConcretePartBase, IConcreteBase
    {
        public enum ConnectToPilesTypeEnum
        {
            YES,
            NO
        }

        public IEnumerable<double> StepsX1;
        public IEnumerable<double> StepsX2;
        public IEnumerable<double> StepsY1;
        public IEnumerable<double> StepsY2;
        public IEnumerable<double> StepsZ;
        public ConnectToPilesTypeEnum ConnectToPilesType;
        public ConcreteFoundation(TSM.Model currentModel)
        {
            _currentModel = currentModel;
            CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;
            _secondaryParts = new List<Part>();
            StepsX1 = new List<double> { 400, 300, 300, 300 };
            StepsY1 = new List<double> { 400, 300, 300, 300 };
            StepsX2 = new List<double> { 400, 300, 300, 300 };
            StepsY2 = new List<double> { 400, 300, 300, 300 };
            StepsZ = new List<double> { 1000, 300, 300, 300 };
            ConnectToPilesType = ConnectToPilesTypeEnum.NO;
        }
        public void Insert()
        {
            GeometryFoundation();
            InsertAssembly();

            if (ConnectToPilesType == ConnectToPilesTypeEnum.YES)
            {
                if (_secondaryParts.Count != 0)
                    ConnectToPiles(_secondaryParts[_secondaryParts.Count - 1] as ContourPlate);
                else
                    ConnectToPiles(_mainPart as ContourPlate);
            }
        }
        private void GeometryFoundation()
        {
            var localZ = 0.0;
            var localX1 = 0.0;
            var localX2 = 0.0;
            var localY1 = 0.0;
            var localY2 = 0.0;

            for (int i = 0; i < StepsZ.Count(); i++)
            {
                if (i == 0)
                {
                    localZ += StepsZ.ElementAt(i) / 2;
                }
                else
                {
                    localZ += (StepsZ.ElementAt(i) + StepsZ.ElementAt(i - 1)) / 2;
                }

                localX1 += GetDouble(StepsX1, i);
                localX2 += GetDouble(StepsX2, i);
                localY1 += GetDouble(StepsY1, i);
                localY2 += GetDouble(StepsY2, i);

                var p1 = new Point(-localX2, -localY2, -localZ);
                var p2 = new Point(localX1, -localY2, -localZ);
                var p3 = new Point(localX1, localY1, -localZ);
                var p4 = new Point(-localX2, localY1, -localZ);

                var foundation = new ContourPlate();
                foundation.Class = Class;
                foundation.Material.MaterialString = Material;
                foundation.Profile.ProfileString = GlobalParameters.BaseProfilePlate + StepsZ.ElementAt(i).ToString();
                foundation.AssemblyNumber.Prefix = AssemblyPrefix;
                foundation.AssemblyNumber.StartNumber = AssemblyStartNumber;
                foundation.PartNumber.Prefix = PartPrefix;
                foundation.PartNumber.StartNumber = PartStartNumber;
                foundation.Name = PartName;
                foundation.Finish = Finish;
                foundation.PourPhase = PourPhase;
                foundation.Position.Plane = Position.PlaneEnum.MIDDLE;
                foundation.Position.Depth = Position.DepthEnum.MIDDLE;
                foundation.CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;

                foundation.AddContourPoint(new ContourPoint(p1, null));
                foundation.AddContourPoint(new ContourPoint(p2, null));
                foundation.AddContourPoint(new ContourPoint(p3, null));
                foundation.AddContourPoint(new ContourPoint(p4, null));

                if (foundation.Insert())
                {
                    if (i == 0)
                        _mainPart = foundation;
                    else
                        _secondaryParts.Add(foundation);
                }
            }
        }
        private double GetDouble(IEnumerable<double> steps, int i)
        {
            try
            {
                return steps.ElementAt(i);
            }
            catch
            {
                return 0.0;
            }
        }
        private void ConnectToPiles(ContourPlate foundation)
        {
            if (foundation == null)
                return;

            var piles = new List<Part>();

            var pointsPiles = new List<Point>();

            var z = (foundation.Contour.ContourPoints[0] as Point).Z;

            foreach (Part pile in piles)
            {
                var pileGeometry = new ModelObjectGeometry(_currentModel, pile);
                pointsPiles.Add(new Point(pileGeometry.MidX, pileGeometry.MidY, z));
            }

            foundation.Contour.ContourPoints.Clear();
            foundation.Contour.ContourPoints.AddRange(pointsPiles);
            foundation.Modify();
        }
    }
}
