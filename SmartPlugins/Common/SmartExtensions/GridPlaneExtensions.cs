using System.Collections.Generic;
using System.Linq;
using tsg = Tekla.Structures.Geometry3d;
using tsm = Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class GridPlaneExtension
    {
        /// <summary>
        /// Получение точки и ее метки сетки. К примеру: "Point(0,0,0) A/1"
        /// </summary>
        /// <param name="gridPlanes"></param>
        /// <returns></returns>
        public static Dictionary<tsg.Point, string> GetPointsFromGridPlanes(this List<tsm.GridPlane> gridPlanes)
        {
            gridPlanes = gridPlanes.Where(x =>
            {
                var vectorZ = x.Plane.AxisX.Cross(x.Plane.AxisY);
                return !(vectorZ.Z > 0);
            }).ToList();
            Dictionary<tsg.Point, string> _axis = new Dictionary<tsg.Point, string>();
            for (int i = 0; i < gridPlanes.Count - 1; i++)
            {
                var g1 = gridPlanes[i];
                var pl1 = g1.Plane;
                var label1 = g1.Label;

                for (int k = 0; k < gridPlanes.Count; k++)
                {
                    if (i == k) continue;
                    var g2 = gridPlanes[k];
                    var pl2 = g2.Plane;
                    var label2 = g2.Label;
                    var l = tsg.Intersection.PlaneToPlane(pl1.ToGeometricPlane(), pl2.ToGeometricPlane());
                    if (l != null)
                    {
                        tsg.Point p = new tsg.Point(l.Origin.X, l.Origin.Y, 0);
                        _axis.Add(p, $"{label1}/{label2}");
                    }
                }
                gridPlanes.RemoveAt(i);
                i--;
            }

            return _axis;
        }
    }
}
