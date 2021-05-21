using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class PartExtension
    {
        public static double SmartGetPropertyDouble(this Part part, string attributeName)
        {
            var result = double.NaN;

            part.GetReportProperty(attributeName, ref result);

            return result;
        }

        public static string SmartGetPropertyString(this Part part, string attributeName)
        {
            var result = string.Empty;

            part.GetReportProperty(attributeName, ref result);

            return result;
        }
    }
}
