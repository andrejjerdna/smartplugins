using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.TeklaLibrary.Points;

namespace SmartPlugins.Common.TeklaLibrary.Parts
{
    public class PartOperations : IPartOperations
    {
        /// <summary>
        /// Reverse part points
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        public void ReverseLocationPointsModelObject<T>(T modelObject) where T : class
        {
            new ReversModelObjectPoints().ReverseLocationPoints(modelObject);
        }
    }
}
