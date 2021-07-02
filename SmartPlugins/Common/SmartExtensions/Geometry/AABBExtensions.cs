
using System;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;
using tsd = Tekla.Structures.Drawing;

namespace SmartExtensions.Geometry
{
    public static class AABBExtensions
    {
        /// <summary>
        /// Длина диагонали коробки
        /// </summary>
        /// <param name="ab"></param>
        /// <returns></returns>
        public static double Diagonal(this AABB ab)
        {
            return Distance.PointToPoint(ab.MinPoint, ab.MaxPoint);
        }
        


        /// <summary>
        /// Проверка что одна AABB лежит внутри другой
        /// </summary>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsInside(this AABB p, AABB i)
        {
            AABB p_new = new AABB(p.Min(), p.Max());
            AABB i_new = new AABB(i.Min(), i.Max());

            bool min = p_new.IsInside(i_new.MinPoint);
            bool max = p_new.IsInside(i_new.MaxPoint);
            bool center = p_new.IsInside(i_new.GetCenterPoint());

            return min && max && center;
        }

        /// <summary>
        /// Получить минимальную точку 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Point Min(this AABB a)
        {
            return new Point
            {
                X = Math.Min(a.MinPoint.X, a.MaxPoint.X),
                Y = Math.Min(a.MinPoint.Y, a.MaxPoint.Y),
                Z = Math.Min(a.MinPoint.Z, a.MaxPoint.Z),
            };
        }

        /// <summary>
        /// Получить максимальную точку
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Point Max(this AABB a)
        {
            return new Point
            {
                X = Math.Max(a.MinPoint.X, a.MaxPoint.X),
                Y = Math.Max(a.MinPoint.Y, a.MaxPoint.Y),
                Z = Math.Max(a.MinPoint.Z, a.MaxPoint.Z),
            };
        }
    }
}
