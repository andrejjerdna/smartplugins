using Tekla.Structures.Geometry3d;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class PointExtension
    {
        /// <summary>
        /// Clone point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point Clone(this Point point)
        {
            return new Point(point.X, point.Y, point.Z);
        }
    }
}
