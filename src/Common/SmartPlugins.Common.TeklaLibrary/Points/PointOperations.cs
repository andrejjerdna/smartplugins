using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.TeklaLibrary.CSLib;
using System;
using Tekla.Structures.Geometry3d;

namespace SmartPlugins.Common.TeklaLibrary.Points
{
    public class PointOperations : IPointOperations
    {
        /// <inheritdoc/>
        public void DrawCoordinatesPoint<T>(T point)
        {
            if (point is Point modelPoint)
                DrawUI.DrawPoint(modelPoint, modelPoint.ToString());
        }

        /// <inheritdoc/>
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
