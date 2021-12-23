namespace SmartPlugins.Common.Abstractions.Geometry
{
    public interface IPartOperations
    {
        /// <summary>
        /// Reverse part points of model object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        void ReverseLocationPointsModelObject<T>(T modelObject) where T : class;

        /// <summary>
        /// Rounding coordinates points of model object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        void RoundingCoordinatesPointsModelObject<T>(T modelObject) where T : class;
    }
}
