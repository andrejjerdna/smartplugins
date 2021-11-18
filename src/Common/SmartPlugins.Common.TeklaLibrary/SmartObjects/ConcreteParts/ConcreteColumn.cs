using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.SmartObjects
{
    public class ConcreteColumn : ConcretePartBase, IConcreteBase
    {
        public enum ConnectToPilesTypeEnum
        {
            YES,
            NO
        }

        public IEnumerable<Part> Plates;

        public IEnumerable<double> StepsX1;
        public IEnumerable<double> StepsX2;
        public IEnumerable<double> StepsY1;
        public IEnumerable<double> StepsY2;
        public IEnumerable<double> StepsZ;
        public ConnectToPilesTypeEnum ConnectToPilesType;
        public ConcreteColumn(TSM.Model currentModel)
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

        }
        private IEnumerable<double> GetLevels()
        {
            var xMaxLevelsPlate = Plates.Select(plate => plate.GetSolid().MaximumPoint);
            var xMinLevelsPlate = Plates.Select(plate => plate.GetSolid().MinimumPoint);

            var xLevelsPlate = xMaxLevelsPlate.Union(xMinLevelsPlate).OrderBy(point => point.X);

            var line = new LineSegment(xLevelsPlate.First(), xLevelsPlate.Last());

           var tr = Plates.Select(plate => plate.GetSolid().Intersect(line).Cast<Point>().OrderBy(point => point.X));
            return new List<double>();
        }
    }
}
