using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

using Tekla.Structures.Geometry3d;
using SmartExtensions.Geometry;

namespace SmartExtensions
{
    public static class BoltsExtensions
    {
        public static IEnumerable<Part> GetAllParts(this BoltGroup bolts)
        {
            var result = new List<Part>();

            result.Add(bolts.PartToBeBolted as Part);
            result.Add(bolts.PartToBoltTo as Part);
            result.AddRange(bolts.OtherPartsToBolt.ToArray().Select(b => b as Part));

            return result.Where(part => part != null);
        }

        /// <summary>
        /// Получить коробку AABB болта по 
        /// координатам позиций
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAabb(this BoltGroup bg)
        {
            Point MinPoint = bg.BoltPositions.GetMinPoint();
            Point MaxPoint = bg.BoltPositions.GetMaxPoint();
            PointExtensions.MinMax(ref MinPoint, ref MaxPoint);
            return new AABB(MinPoint, MaxPoint);
        }

        /// <summary>
        /// Получить OBB болтовой группы с допуском
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static OBB GetbObb(this BoltGroup bg, double distance = 1)
        {
            var minPoint = bg.BoltPositions.GetMinPoint();
            var maxPoint = bg.BoltPositions.GetMaxPoint();

            minPoint -= new Point(distance, distance, distance);
            maxPoint += new Point(distance, distance, distance);

            var centerPoint = minPoint.GetCenterPoint(maxPoint);

            var extent0 = (maxPoint.X - minPoint.X) / 2;
            var extent1 = (maxPoint.Y - minPoint.Y) / 2;
            var extent2 = (maxPoint.Z - minPoint.Z) / 2;

            var cs = bg.GetCoordinateSystem();
            return new OBB(centerPoint, cs.AxisX, cs.AxisY, cs.AxisZ(), extent0, extent1, extent2);
        }

        /// <summary>
        /// Получить точки в виде списка
        /// </summary>
        /// <param name="bg"></param>
        /// <returns></returns>
        public static List<Point> GetStartEndPoints(this BoltGroup bg)
        {
            return new List<Point>()
            {
                bg.FirstPosition,
                bg.SecondPosition
            };
        }
    }
}
