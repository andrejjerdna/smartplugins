using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;

namespace SmartExtensions.Geometry
{
    public static class CoordinateSystemExtensions
    {
        public static Vector AxisZ(this CoordinateSystem cs)
        {
            return cs.AxisX.Cross(cs.AxisY);
        }
    }
}
