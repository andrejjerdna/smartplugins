using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartTeklaModel.Drawings
{
    public static class Colors
    {
        public static IEnumerable<DrawingColors> GetListColors()
        {
            var result = new List<DrawingColors>();
            foreach (var item in Enum.GetValues(typeof(DrawingColors)))
            {
                if(item is DrawingColors color)
                result.Add(color);
            }
            return result;
        }

        public static IEnumerable<DrawingHatchColors> GetListHatchColors()
        {
            var result = new List<DrawingHatchColors>();
            foreach (var item in Enum.GetValues(typeof(DrawingHatchColors)))
            {
                if (item is DrawingHatchColors color)
                    result.Add(color);
            }
            return result;
        }


        public static DrawingColors GetColor(string colorName)
        {
            var colors = GetListColors();

            var index = colors.Where(c => c.ToString() == colorName);

            if (index.Count() > 0)
                return index.First();
            else
                return colors.First();
        }

        public static DrawingHatchColors GetHatchColor(string colorName)
        {
            var colors = GetListHatchColors();

            var index = colors.Where(c => c.ToString() == colorName);

            if (index.Count() > 0)
                return index.First();
            else
                return colors.First();
        }
    }
}
