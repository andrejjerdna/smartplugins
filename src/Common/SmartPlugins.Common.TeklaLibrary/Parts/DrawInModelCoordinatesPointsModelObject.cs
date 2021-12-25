using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Parts
{
    internal class DrawInModelCoordinatesPointsModelObject
    {
        private readonly IPointOperations _pointOperations;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="pointOperations"></param>
        public DrawInModelCoordinatesPointsModelObject(IPointOperations pointOperations)
        {
            _pointOperations = pointOperations;
        }

        /// <summary>
        /// Draw coordinates for all points
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        public void DrawAllPoints<T>(T modelObject)
        {
            if (modelObject is Part part)
            {
                var points = part.GetAllPoints();

                foreach (var point in points)
                    _pointOperations.DrawCoordinatesPoint(point);
            }
        }
    }
}
