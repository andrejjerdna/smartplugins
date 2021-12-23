namespace SmartPlugins.Common.Abstractions.Geometry
{
    public interface IPartOperations
    {
        /// <summary>
        /// Reverse part points
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        void ReverseLocationPointsModelObject<T>(T modelObject) where T : class;
    }
}
