using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.TeklaLibrary.Points;

namespace SmartPlugins.Common.TeklaLibrary.Parts
{
    public class PartOperations : IPartOperations
    {
        private readonly IPointOperations _pointOperations;

        public PartOperations(IPointOperations pointOperations)
        {
            _pointOperations = pointOperations;
        }

        /// <inheritdoc/>
        public void ReverseLocationPointsModelObject<T>(T modelObject) where T : class
        {
            new ReversModelObjectPoints().ReverseLocationPoints(modelObject);
        }

        /// <inheritdoc/>
        public void RoundingCoordinatesPointsModelObject<T>(T modelObject) where T : class
        {
            new RoundingCoordinatesPointsModelObject(_pointOperations).RoundingPoints(modelObject);
        }
    }
}
