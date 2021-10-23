using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Datatype;

namespace SmartPlugins.Common.TeklaLibrary
{
    public class BoltsMethods
    {
        /// <summary>
        /// Получаем список шагов болтов в соединении из заданного пользователем набора.
        /// </summary>
        /// <param name="distenceString"></param>
        /// <returns></returns>
        public static IEnumerable<double> GetDistanceList(string distenceString)
        {
            return DistanceList.Parse(distenceString, CultureInfo.InvariantCulture, Distance.UnitType.Millimeter)
                .Cast<Distance>()
                .Select(distance => distance.ConvertTo(Distance.UnitType.Millimeter))
                .OfType<double>()
                .ToList();
        }
    }
}
