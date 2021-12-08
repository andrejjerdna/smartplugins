using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class PointExtensions
    {
        /// <summary>
        /// Transform current point between transformation planes
        /// </summary>
        /// <param name="point"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Point TransformationPoint(this Point point, TransformationPlane from, TransformationPlane to)
        {
            return to.TransformationMatrixToLocal.Transform(from.TransformationMatrixToGlobal.Transform(point));
        }
    }
}
