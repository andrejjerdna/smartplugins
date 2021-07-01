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

        /// <summary>
        /// Получить OBB детали
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static OBB GetObb(this tsd.Part part)
        {
            return part.GetMPart().GetObb();
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
        /// Получить OBB коробки относительно CS (опционально),
        /// </summary>
        /// <param name="part"></param>
        /// <param name="cs">Если null то используется локальная система</param>
        /// <returns></returns>
        public static OBB GetObb(this Part part, CoordinateSystem cs = null)
        {
            // взято с api reference
            var workPlaneHandler = new Model().GetWorkPlaneHandler();
            var originalTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();

            var solid = part.GetSolid();
            var minPointInCurrentPlane = solid.MinimumPoint;
            var maxPointInCurrentPlane = solid.MaximumPoint;

            var centerPoint = minPointInCurrentPlane.GetCenterPoint(maxPointInCurrentPlane);

            var coordSys = part.GetCoordinateSystem();
            var localTransformationPlane = new TransformationPlane(cs ?? coordSys);
            workPlaneHandler.SetCurrentTransformationPlane(localTransformationPlane);

            solid = part.GetSolid();
            var minPoint = solid.MinimumPoint;
            var maxPoint = solid.MaximumPoint;
            var extent0 = (maxPoint.X - minPoint.X) / 2;
            var extent1 = (maxPoint.Y - minPoint.Y) / 2;
            var extent2 = (maxPoint.Z - minPoint.Z) / 2;

            workPlaneHandler.SetCurrentTransformationPlane(originalTransformationPlane);

            return new OBB(centerPoint, coordSys.AxisX, coordSys.AxisY, coordSys.AxisX.Cross(coordSys.AxisY), extent0, extent1, extent2);

        }

        /// <summary>
        /// Получить общую OBB деталей
        /// </summary>
        /// <param name="list"></param>
        /// <param name="kf">Множитель, по-умолчанию равен 1</param>
        /// <returns></returns>
        public static OBB GetCommonObb(this List<Part> list, double kf = 1)
        {
            var result = new OBB(list[0].GetObb());

            var ab = new AABB(result.GetMinPoint(), result.GetMaxPoint());
            var center = result.Center;

            for (var i = 1; i < list.Count; i++)
            {
                var lab = list[i].GetAabb();
                var lcenter = lab.GetCenterPoint();

                center = new Point(center.GetCenterPoint(lcenter));

                ab = ab + lab;
            }

            var ext0 = (ab.Max().X - ab.Min().X) / 2;
            var ext1 = (ab.Max().Y - ab.Min().Y) / 2;
            var ext2 = (ab.Max().Z - ab.Min().Z) / 2;


            ext0 *= kf;
            ext1 *= kf;
            ext2 *= kf;

            result = new OBB(center, result.Axis0, result.Axis1, result.Axis2, ext0, ext1, ext2);
            return result;
        }
    }
}
