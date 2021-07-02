using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures.Model;
using tsg = Tekla.Structures.Geometry3d;

namespace SmartExtensions
{
    public static class ModelObjectExtension
    {
        public static double SmartGetPropertyDouble(this ModelObject mo, string attributeName)
        {
            var result = 0.0;
            mo.GetReportProperty(attributeName, ref result);
            return result;
        }

        public static double SmartGetPropertyInt(this ModelObject mo, string attributeName)
        {
            var result = 0;
            mo.GetReportProperty(attributeName, ref result);
            return result;
        }

        public static string SmartGetPropertyString(this ModelObject mo, string attributeName)
        {
            var result = string.Empty;
            mo.GetReportProperty(attributeName, ref result);
            return result;
        }

        /// <summary>
        /// Возвращение Локальной системы координат для некоторых типов объектов
        /// из общего объекта mobj 
        /// </summary>
        /// <param name="mobj"></param>
        /// <returns></returns>
        public static tsg.CoordinateSystem GetCoordinateSystem(this ModelObject mobj)
        {
            tsg.CoordinateSystem result = null;

            var typeObject = mobj.GetType();

            if (typeObject == typeof(Part))
            {
                result = ((Part)mobj).GetCoordinateSystem();
            }
            else if (typeObject == typeof(Assembly))
            {
                result = ((Assembly)mobj).GetCoordinateSystem();
            }
            else if (typeObject == typeof(BoltGroup))
            {
                result = ((BoltGroup)mobj).GetCoordinateSystem();
            }
            else if (typeObject == typeof(Weld))
            {
                result = ((Weld)mobj).GetCoordinateSystem();
            }
            else if (typeObject == typeof(RebarGroup))
            {
                result = ((RebarGroup)mobj).GetCoordinateSystem();
            }

            return result;
        }
    }
}
