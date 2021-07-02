using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures.Geometry3d;

using SmartExtensions.Geometry;

namespace SmartExtensions
{
    public static class ArrayListExtensions
    {
        /// <summary>
        /// Перевод точек из ArrayList в List
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static List<Point> ToPointList(this ArrayList position)
        {
            List<Point> points = new List<Point>();
            foreach (var k in position)
            {
                points.Add(k as Point);
            }

            return points;
        }


        /// <summary>
        /// Получить центральную точку из ArrayList точек
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetCogFromPointList(this ArrayList positions)
        {
            return positions.GetMinPoint().GetCenterPoint(positions.GetMaxPoint());
        }

        /// <summary>
        /// Получить минимальную точку из ArrayList
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMinPoint(this ArrayList positions)
        {
            var min = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                min = (k as Point).Min(min);
            }

            return min;
        }

        /// <summary>
        /// Получить максимальную точку из ArrayList
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMaxPoint(this ArrayList positions)
        {
            var max = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                max = (k as Point).Max(max);
            }

            return max;
        }

        /// <summary>
        /// Перевод точек из ArrayList в List
        /// с дополнительной трансформацией
        /// </summary>
        /// <param name="position"></param>
        /// <param name="vMatrix"></param>
        /// <returns></returns>
        public static List<Point> ToList(this ArrayList position, Matrix vMatrix)
        {
            List<Point> points = new List<Point>();
            foreach (var k in position)
            {
                points.Add(vMatrix.Transform(k as Point));
            }

            return points;
        }
    }
}
