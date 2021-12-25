using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class PartExtension
    {
        /// <summary>
        /// Получаем всю арматуру детали.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="getMainPart">Надо ли получать главную деталь сборки.</param>
        /// <returns></returns>
        public static IEnumerable<Reinforcement> GetAllReinforcements(this Part part)
        {
            return part.GetReinforcements().ToIEnumerable<Reinforcement>();
        }

        /// <summary>
        /// Gell all points of part
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public static IEnumerable<Point> GetAllPoints(this Part part)
        {
            if(part is Beam beam)
                return new List<Point> { beam.StartPoint, beam.EndPoint };

            if (part is PolyBeam polyBeam)
                return polyBeam.Contour.ContourPoints.OfType<Point>();

            if (part is ContourPlate contourPlate)
                return contourPlate.Contour.ContourPoints.OfType<Point>();

            return Enumerable.Empty<Point>();
        }
    }
}
