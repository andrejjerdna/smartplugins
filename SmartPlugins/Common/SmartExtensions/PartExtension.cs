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
    public static class PartExtension
    {
        /// <summary>
        /// Получаем всю арматуру детали.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<Reinforcement> GetAllReinforcements(this Part part)
        {
            return part.GetReinforcements().ToIEnumerable<Reinforcement>();
        }

        /// <summary>
        /// Получение COG детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point GetCOG(this Part p)
        {
            return p.GetAABB().GetCenterPoint();
        }


        /// <summary>
        /// Получить коробку AABB детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAABB(this Part p)
        {
            var min = p.GetSolid().MinimumPoint;
            var max = p.GetSolid().MaximumPoint;
            return new AABB(min, max);
        }

        /// <summary>
        /// Проверка на однодетальную марку-сборку
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool isSingleAssembly(this Part part)
        {
            return part.GetAssembly().GetSecondaries().Count == 0;
        }

        /// <summary>
        /// Проверка на однодетальную марку-сборку
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static bool isSingleAssembly(this List<Part> parts)
        {
            foreach (var p in parts)
            {
                if (isSingleAssembly(p) == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получение списка однодетальных марок.
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetSingleAssemblies(this List<Part> parts)
        {
            return parts.Where(a => a.isSingleAssembly()).Select(a => a.GetAssembly());
        }

        /// <summary>
        /// Получение списка сборок из списка деталей,
        /// Сборки не повторяются, если детали принадлежат к одной
        /// Сравнение идет по идентификатору
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblies(this List<Part> parts)
        {
            List<Assembly> result = new List<Assembly>();
            List<int> asmIDS = new List<int>();
            foreach (var p in parts)
            {
                if (p.isSingleAssembly())
                {
                    result.Add(p.GetAssembly());
                }
                else
                {
                    int id = p.GetAssembly().Identifier.ID;
                    if (asmIDS.Contains(id)) continue;

                    result.Add(p.GetAssembly());
                    asmIDS.Add(id);
                }
            }

            return result.AsEnumerable();
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
        /// Получить все точки детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IEnumerable<Point> GetPoints(this Part p)
        {
            return p.GetCenterLine(true).ToPointList().AsEnumerable();
        }
    }
}
