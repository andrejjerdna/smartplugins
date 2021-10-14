using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartPlugins.Common.SmartTeklaModel.Drawings
{
    public static class Lines
    {
        public static IEnumerable<LineTypes> GetListLineTypes()
        {
            return new List<LineTypes>()
            {
                LineTypes.DashDot,
                LineTypes.DashDoubleDot,
                LineTypes.SolidLine,
                LineTypes.DashedLine,
                LineTypes.DottedLine,
                LineTypes.SlashDash,
                LineTypes.SlashedLine,
                LineTypes.UndefinedLine,
            };
        }

        public static LineTypes GetLineTypes(string typeName)
        {
            var colors = GetListLineTypes();

            var index = colors.Where(c => c.ToString() == typeName);

            if (index.Count() > 0)
                return index.First();
            else
                return colors.First();
        }
    }
}
