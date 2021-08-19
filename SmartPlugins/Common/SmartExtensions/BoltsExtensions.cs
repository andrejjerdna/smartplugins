using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartExtensions
{
    public static class BoltsExtensions
    {
        public static IEnumerable<Part> GetAllParts(this BoltGroup bolts)
        {
            var result = new List<Part>();

            result.Add(bolts.PartToBeBolted as Part);
            result.Add(bolts.PartToBoltTo as Part);
            result.AddRange(bolts.OtherPartsToBolt.ToArray().Select(b => b as Part));

            return result.Where(part => part != null);
        }
    }
}
