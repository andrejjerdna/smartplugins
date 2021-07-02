using System.Collections.Generic;
using System.Data;

using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

using tsd = Tekla.Structures.Drawing;

namespace SmartExtensions.Geometry
{
    public static class OBBExtensions
    {
        /// <summary>
        /// Пересечение OBB c допуском
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static bool IsCollide(this OBB current, OBB other, double distance = 1000)
        {
            var matrix = MatrixFactory.ToCoordinateSystem(current.GetCoordinateSystem());

            var cmin = matrix.Transform(current.GetMinPoint());
            var cmax = matrix.Transform(current.GetMaxPoint());

            var omin = matrix.Transform(other.GetMinPoint());
            var ofmax = matrix.Transform(other.GetMaxPoint());

            bool minmin = Distance.PointToPoint(cmin, omin) <= distance;
            bool minmax = Distance.PointToPoint(cmin, ofmax) <= distance;
            bool maxmin = Distance.PointToPoint(cmax, omin) <= distance;
            bool maxmax = Distance.PointToPoint(cmax, ofmax) <= distance;

            return minmin || minmax || maxmin || maxmax;
        }

        /// <summary>
        /// Получить CS OBB
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public static CoordinateSystem GetCoordinateSystem(this OBB current)
        {
            return new CoordinateSystem(current.Center, current.Axis0, current.Axis1);
        }

        /// <summary>
        /// Проверка что OBB входит в другую
        /// </summary>
        /// <param name="th"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsInside(this OBB th, OBB other)
        {
            //return th.Intersects(other);

            var m = MatrixFactory.ToCoordinateSystem(new CoordinateSystem(th.Center, th.Axis0, th.Axis1));
            var min = m.Transform(th.GetMinPoint());
            var max = m.Transform(th.GetMaxPoint());

            PointExtensions.MinMax(ref min, ref max);

            var omin = m.Transform(other.GetMinPoint().Min(other.GetMaxPoint()));
            var omax = m.Transform(other.GetMinPoint().Max(other.GetMaxPoint()));

            var bmin = omin.IsInside(min, max);
            var bmax = omax.IsInside(min, max);



            return bmin && bmax;
        }

        /// <summary>
        /// Получить минимальную точку
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point GetMinPoint(this OBB b)
        {
            return new Point
            {
                X = b.Center.X - b.Extent0,
                Y = b.Center.Y - b.Extent1,
                Z = b.Center.Z - b.Extent2,
            };
        }
        
        /// <summary>
        /// Получить максимальную точку
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point GetMaxPoint(this OBB b)
        {
            return new Point
            {
                X = b.Center.X + b.Extent0,
                Y = b.Center.Y + b.Extent1,
                Z = b.Center.Z + b.Extent2,
            };
        }

    }
}
