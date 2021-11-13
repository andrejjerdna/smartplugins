using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSM = Tekla.Structures.Model;
using SmartPlugins.Common.SmartGeometry;
using SmartPlugins.Common.TeklaLibrary;

namespace SmartPlugins.Common.SmartObjects
{
    /// <summary>
    /// Подливка под опорную плиту металлической колонны.
    /// </summary>
    public sealed class GroutUnderBasePlate : ConcretePartBase, IConcreteBase
    {
        public enum EdgeTypeEnum
        {
            YES,
            NO
        }
        private Part ColumnBasePlate { get; set; }
        private Part Foundation { get; set; }
        /// <summary>
        /// Выступ за грань опорной плиты колонны.
        /// </summary>
        private double Offcet;
        public EdgeTypeEnum EdgeType { get; set; }
        /// <summary>
        /// Толщина создаваемой бетонной подливки.
        /// </summary>
        private double ThicknessPlate;
        public GroutUnderBasePlate(TSM.Model currentModel, Part foundation, Part columnBasePlate, double equidistant)
        {
            _currentModel = currentModel;
            Foundation = foundation;
            ColumnBasePlate = columnBasePlate;
            Offcet = equidistant;
            AddToCastUnit = AssemblyTypeEnum.NOT_ADD_TO_CAST_UNIT;
            EdgeType = EdgeTypeEnum.NO;
            ThicknessPlate = Thickness();
        }
        public void Insert()
        {
            _mainPart = InsertPlate();

            if (EdgeType == EdgeTypeEnum.YES)
            {
                CreateEdge();
            }

            if (AddToCastUnit == AssemblyTypeEnum.ADD_TO_CAST_UNIT)
            {
                AddToAssembly(Foundation);
            }
            else
            {
                DeleteToAssembly(Foundation);
            }
        }
        private void CreateEdge()
        {
            var mainPartGeometry = new ModelObjectGeometry(_mainPart);

            var edge = new ContourPlateEdge(_mainPart, ThicknessPlate, Offcet, mainPartGeometry.MaxZ);
            edge.Insert();

        }
        /// <summary>
        /// Добавление подливки к сборке фундамента.
        /// </summary>

        private Part InsertPlate()
        {
            var polygon = IntersectWithSolid.PolygonEquidistant(ColumnBasePlate, Offcet);

            var groutUnderBasePlate = new ContourPlate();
            groutUnderBasePlate.Class = Class;
            groutUnderBasePlate.Material.MaterialString = Material;
            groutUnderBasePlate.Profile.ProfileString = GlobalParameters.BaseProfilePlate + ThicknessPlate.ToString();
            groutUnderBasePlate.AssemblyNumber.Prefix = AssemblyPrefix;
            groutUnderBasePlate.AssemblyNumber.StartNumber = AssemblyStartNumber;
            groutUnderBasePlate.PartNumber.Prefix = PartPrefix;
            groutUnderBasePlate.PartNumber.StartNumber = PartStartNumber;
            groutUnderBasePlate.Name = PartName;
            groutUnderBasePlate.Finish = Finish;
            groutUnderBasePlate.PourPhase = PourPhase;
            groutUnderBasePlate.Position.Plane = Position.PlaneEnum.RIGHT;
            groutUnderBasePlate.Position.Depth = Position.DepthEnum.MIDDLE;
            groutUnderBasePlate.CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;

            foreach (Point p in polygon.Points)
            {
                p.Z = ColumnBasePlate.GetSolid().MinimumPoint.Z - ThicknessPlate / 2;
                groutUnderBasePlate.AddContourPoint(new ContourPoint(p, null));
            }

            groutUnderBasePlate.Insert();

            return groutUnderBasePlate;
        }
        /// <summary>
        /// Получаем толщину бетонной подливки.
        /// </summary>
        /// <returns></returns>
        private double Thickness()
        {
            var line = new LineSegment(new Point(0, 0, 1000), new Point(0, 0, -1000));

            var intersectColumnBasePlate = ColumnBasePlate.GetSolid().Intersect(line).OfType<Point>().OrderBy(point => point.Z);
            var intersectFoundation = Foundation.GetSolid().Intersect(line).OfType<Point>().OrderBy(point => point.Z);

            if(intersectColumnBasePlate.Count() == 0 || intersectFoundation.Count() == 0)
            {
                //MessageBox.Show("No intersection!");
                return 10;
            }

            var result = new LineSegment(intersectColumnBasePlate.First(), intersectFoundation.Last()).Length();

            return Math.Round(result, 2);
        }
        
    }
}
