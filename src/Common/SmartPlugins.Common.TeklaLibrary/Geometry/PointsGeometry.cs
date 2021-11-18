using System;
using Tekla.Structures.Geometry3d;

namespace SmartPlugins.Common.TeklaLibrary.Geometry
{
    public static class PointsGeometry
    {
        /// <summary>
        /// Get dist between two points
        /// </summary>
        /// <param name="p1">Point 1</param>
        /// <param name="p2">Point 2</param>
        /// <returns></returns>
        public static double GetDistanceBetweenPoints(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X)* (p1.X - p2.X) + (p1.Y - p2.Y)* (p1.Y - p2.Y));
        }

        public static Point IntersectLinesByPoints(Point p1, Point p2, Point p3, Point p4)
        {
            // return Math.Sqrt((p1.X * p1.X - p2.X * p2.X) + (p1.Y * p1.Y - p2.Y * p2.Y));
            return new Point();
        }

        public static Point IntersectLinesByPoints(Line l1, Line l2)
        {
            return IntersectLinesByPoints(l1.Origin, l1.Direction, l2.Origin, l2.Direction);
        }
    }
}
