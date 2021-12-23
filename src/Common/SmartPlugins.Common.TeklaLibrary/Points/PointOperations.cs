using SmartPlugins.Common.Abstractions.Geometry;
using System;
using Tekla.Structures.Geometry3d;

namespace SmartPlugins.Common.TeklaLibrary.Points
{
    public class PointOperations : IPointOperations
    {
        /// <summary>
        /// Rounding сoordinates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="point"></param>
        /// <exception cref="Exception"></exception>
        public void RoundingCoordinates<T>(T point)
        {
            if (point is Point modelPoint)
            {
                modelPoint.X = Math.Round(modelPoint.X, 0);
                modelPoint.Y = Math.Round(modelPoint.Y, 0);
                modelPoint.Z = Math.Round(modelPoint.Z, 0);
                return;
            }

            throw new Exception();
        }
    }
}
