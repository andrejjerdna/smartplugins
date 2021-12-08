using SmartPlugins.Common.TeklaLibrary.CSLib;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Geometry
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
        public static Polygon PolygonEquidistant(Part part, double offset = 0.0)
        {
            var outmostPoints = OutmostPoints(part, new Point(0, 0, 0), new Point(1000, 0, 0), new Point(0, 1000, 0), Solid.SolidCreationTypeEnum.RAW);
            var result = new Polygon();
            result.Points.AddRange(outmostPoints.ToArray());

            if (Math.Abs(offset) > 0.0001)
            {
                var offsetList = new List<double>() { -offset };
                PolygonOperation.PolygonOffset(result, offsetList, true, false);
            }
            return result;
        }

        /// <summary>
        /// Получаем пересечение плоскости по трем точкам и Solid детали.
        /// </summary>
        /// <param name="part">Деталь, с которой ищется пересечение.</param>
        /// <param name="solidType">Тип Solid.</param>
        /// <returns></returns>
        public static IEnumerable<Point> OutmostPoints(Part part, Point p1, Point p2, Point p3, Solid.SolidCreationTypeEnum solidType)
        {
            return (part.GetSolid(solidType)
                        .IntersectAllFaces(p1, p2, p3)
                        .ToIEnumerable<ArrayList>()
                        .First()[0] as ArrayList).OfType<Point>().ToList();
        }
    }
}
