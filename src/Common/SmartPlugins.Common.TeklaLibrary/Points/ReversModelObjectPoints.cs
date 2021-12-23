using SmartPlugins.Common.TeklaLibrary.Extensions;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Points
{
    public class ReversModelObjectPoints
    {
        /// <summary>
        /// Reverse location of points for object
        /// </summary>
        /// <param name="modelObject"></param>
        public void ReverseLocationPoints<T>(T modelObject)
        {
            if (modelObject is Beam beam)
                ReverseLocationPoints(beam);

            if (modelObject is PolyBeam polyBeam)
                ReverseLocationPoints(polyBeam);

            if (modelObject is ContourPlate contourPlate)
                ReverseLocationPoints(contourPlate);
        }

        /// <summary>
        /// Reverse location of points for the beam
        /// </summary>
        /// <param name="beam"></param>
        private void ReverseLocationPoints(Beam beam)
        {
            var startPoint = beam.StartPoint.Clone();
            var endPoint = beam.EndPoint.Clone();

            beam.StartPoint = endPoint;
            beam.EndPoint = startPoint;

            beam.Modify();
        }

        /// <summary>
        /// Reverse location of points for the polybeam
        /// </summary>
        /// <param name="polyBeam"></param>
        public void ReverseLocationPoints(PolyBeam polyBeam)
        {
            polyBeam.Contour.ContourPoints.Reverse();
            polyBeam.Modify();
        }

        /// <summary>
        /// Reverse location of points of the contourplate
        /// </summary>
        /// <param name="contourPlate"></param>
        public void ReverseLocationPoints(ContourPlate contourPlate)
        {
            contourPlate.Contour.ContourPoints.Reverse();
            contourPlate.Modify();
        }
    }
}
