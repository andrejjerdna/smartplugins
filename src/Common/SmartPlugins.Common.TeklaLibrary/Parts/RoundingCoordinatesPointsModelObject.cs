using SmartPlugins.Common.Abstractions.Geometry;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Parts
{
    /// <summary>
    /// Rouding coordinates points of model object
    /// </summary>
    public class RoundingCoordinatesPointsModelObject
    {
        private readonly IPointOperations _pointOperations;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="pointOperations"></param>
        public RoundingCoordinatesPointsModelObject(IPointOperations pointOperations)
        {
            _pointOperations = pointOperations;
        }

        public void RoundingPoints<T>(T modelObject)
        {
            if (modelObject is Beam beam)
                RoundingPoints(beam);

            if (modelObject is PolyBeam polyBeam)
                RoundingPoints(polyBeam);

            if (modelObject is ContourPlate contourPlate)
                RoundingPoints(contourPlate);
        }

        /// <summary>
        /// Rouding points for the beam
        /// </summary>
        /// <param name="beam"></param>
        private void RoundingPoints(Beam beam)
        {
            _pointOperations.RoundingCoordinates(beam.StartPoint);
            _pointOperations.RoundingCoordinates(beam.EndPoint);

            beam.Modify();
        }

        /// <summary>
        /// Rouding points of points for the polybeam
        /// </summary>
        /// <param name="polyBeam"></param>
        public void RoundingPoints(PolyBeam polyBeam)
        {
            foreach(var point in polyBeam.Contour.ContourPoints.OfType<Point>())
                 _pointOperations.RoundingCoordinates(point);

            polyBeam.Modify();
        }

        /// <summary>
        /// Rouding points of points of the contourplate
        /// </summary>
        /// <param name="contourPlate"></param>
        public void RoundingPoints(ContourPlate contourPlate)
        {
            foreach (var point in contourPlate.Contour.ContourPoints.OfType<Point>())
                _pointOperations.RoundingCoordinates(point);

            contourPlate.Modify();
        }
    }
}
