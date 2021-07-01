
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
        /// Получить коробку AABB детали из чертежа
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAabb(this tsd.Part p)
        {
            var part = new Model().SelectModelObject(p.ModelIdentifier) as Part;
            return part.GetAabb();
        }
        /// <summary>
        /// Получить коробку AABB детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAabb(this Part p)
        {
            var min = p.GetSolid().MinimumPoint;
            var max = p.GetSolid().MaximumPoint;
            return new AABB(min, max);
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
        /// Получить коробку AABB сборки
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAabb(this Assembly asm)
        {
            var main = (asm.GetMainPart() as Part).GetAabb();
            foreach (var p in asm.GetSecondaries())
            {
                main += (p as Part).GetAabb();
            }
            return main;
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


    }
}
