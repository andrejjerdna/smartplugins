using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tekla.Structures.Geometry3d;
using tsd = Tekla.Structures.Drawing;
using Tekla.Structures.Model;

using SmartExtensions.Geometry;

namespace SmartExtensions.Drawing
{
    public static class DrawingPartExtensions
    {
        /// <summary>
        /// Получить коробку AABB детали из чертежа
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static AABB GetAABB(this tsd.Part p)
        {
            var part = new Model().SelectModelObject(p.ModelIdentifier) as Part;
            return part.GetAABB();
        }

        /// <summary>
        /// Получить OBB детали
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static OBB GetObb(this tsd.Part part)
        {
            return part.GetModelPart().GetObb();
        }

        public static Part GetModelPart(this tsd.Part part)
        {
            return part.ModelIdentifier.ToModel<Part>();
        }
    }
}
