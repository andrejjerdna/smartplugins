using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tsg = Tekla.Structures.Geometry3d;
using tsm = Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class ModelPlaneExtension
    {
        public static tsg.GeometricPlane ToGeometricPlane(this tsm.Plane pl)
        {
            return new tsg.GeometricPlane(pl.Origin, pl.AxisX, pl.AxisY);
        }
    }
}
