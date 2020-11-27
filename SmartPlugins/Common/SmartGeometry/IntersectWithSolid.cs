using CSLib;
using SmartExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartGeometry
{
    public static class IntersectWithSolid
    {
        /// <summary>
        /// Получаем полигон точек с офсеток равномерно увеличивающим контур во все стороны.
        /// Полигон образован пересечением плоскости XY и Solid детали.
        /// </summary>
        /// <param name="part">Деталь, с которой ищется пересечение.</param>
        /// <param name="offset">Офсет.</param>
        /// <returns></returns>
        public static Polygon PolygonEquidistant(Part part, double offset)
        {
            var outmostPoints = OutmostPoints(part, Solid.SolidCreationTypeEnum.RAW);
            var result = new Polygon();
            result.Points.AddRange(outmostPoints.ToArray());
            var offsetList = new List<double>() { -offset };
            PolygonOperation.PolygonOffset(result, offsetList, true, false);
            return result;
        }
        /// <summary>
        /// Получаем пересечение плоскости XY и Solid детали.
        /// </summary>
        /// <param name="part">Деталь, с которой ищется пересечение.</param>
        /// <param name="solidType">Тип Solid.</param>
        /// <returns></returns>
        public static IEnumerable<Point> OutmostPoints(Part part, Solid.SolidCreationTypeEnum solidType)
        {
            return (part.GetSolid(solidType)
                .IntersectAllFaces(new Point(0, 0, 0), new Point(1000, 0, 0), new Point(0, 1000, 0))
                .ToIEnumerable<ArrayList>()
                .First()[0] as ArrayList).OfType<Point>().ToList();
        }
    }
}
