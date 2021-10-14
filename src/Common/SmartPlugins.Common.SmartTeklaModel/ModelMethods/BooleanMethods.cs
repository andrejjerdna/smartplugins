using SmartPlugins.Common.CSLib;
using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    class BooleanMethods
    {
        public static IEnumerable<BooleanPart> GetBoolean(Part part)
        {
            return part.GetBooleans().ToIEnumerable<BooleanPart>();
        }
        public static IEnumerable<Part> GetOperativeParts(Part part)
        {
            return GetBoolean(part).Select(boolean => boolean.OperativePart);
        }
        /// <summary>
        /// Получаем вырезающую пластину, которая образована пересечением плоксости (три точки) и solid тела существующего выреза.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="solid"></param>
        /// <param name="userOffset"></param>
        /// <returns></returns>
        public static ContourPlate GetBooleanOffset(Point p1, Point p2, Point p3, Solid solid, double userOffset)
        {
            var intersect = solid.IntersectAllFaces(p1, p2, p3);

            var list = new List<ArrayList>();
            while (intersect.MoveNext())
            {
                list.Add(intersect.Current as ArrayList);
            }

            var rrr = list[0] as ArrayList;

            var eee = rrr[0] as ArrayList;

            var r = eee.Cast<Point>().ToList();

            var polygon = new Polygon();
            polygon.Points.AddRange(r);

            var offset = new List<double>() { -userOffset };
            PolygonOperation.PolygonOffset(polygon, offset, true, false);

            var cp = new ContourPlate();
            cp.Material.MaterialString = "Concrete_Undefined";
            cp.Profile.ProfileString = "1000";

            foreach (Point p in polygon.Points)
            {
                cp.AddContourPoint(new ContourPoint(p, null));
            }

            cp.Insert();

            return cp;
        }
        public static void CreateBoolean(Part operativePart, ModelObject modelObject, double userOffset)
        {
            var booleanDetail = GetBooleanOffset(new Point(), new Point(1000, 0, 0), new Point(0, 1000, 0), operativePart.GetSolid(), userOffset);

            var boolean = new ContourPlate
            {
                Contour = booleanDetail.Contour,
                Profile = booleanDetail.Profile,
                Position = booleanDetail.Position,
                Class = BooleanPart.BooleanOperativeClassName
            };

            boolean.Insert();

            var booleanPart = new BooleanPart();
            booleanPart.Father = modelObject;
            booleanPart.SetOperativePart(boolean);
            booleanPart.Insert();

            boolean.Delete();

            booleanDetail.Delete();
        }
        public static void InsertBoolean(IEnumerable<Part> operativeParts, Part part, double userOffset)
        {
            foreach (Part operativePart in operativeParts)
            {
                CreateBoolean(operativePart, part, userOffset);
            }
        }
    }
}
