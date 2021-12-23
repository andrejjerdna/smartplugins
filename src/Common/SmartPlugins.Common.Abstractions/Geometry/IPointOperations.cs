namespace SmartPlugins.Common.Abstractions.Geometry
{
    public interface IPointOperations
    {
        /// <summary>
        /// Rounding coordinates for point
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="point"></param>
        void RoundingCoordinates<T>(T point);
    }
}
