using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartExtensions
{
    public static class WeldsExtensions
    {
        public static IEnumerable<Part> GetAllParts(this BaseWeld weld)
        {
            var result = new List<Part>();

            result.Add(weld.MainObject as Part);
            result.Add(weld.SecondaryObject as Part);

            return result.Where(part => part != null);
        }
    }
}
