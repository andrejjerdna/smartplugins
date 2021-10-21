using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartExtensions
{
    public static class ModelObjectExtension
    {
        public static double SmartGetPropertyDouble(this ModelObject mo, string attributeName)
        {
            var result = 0.0;
            mo.GetReportProperty(attributeName, ref result);
            return result;
        }

        public static int SmartGetPropertyInt(this ModelObject mo, string attributeName)
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
    }
}
