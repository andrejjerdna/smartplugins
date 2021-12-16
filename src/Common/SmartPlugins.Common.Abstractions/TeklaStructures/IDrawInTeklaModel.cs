namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    public interface IDrawInTeklaModel
    {
        /// <summary>
        /// Draw tekla object coordinate system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectCoordinateSystem"></param>
        void DrawTeklaObjectCoordinateSystem<T>(T coordinateSystem) where T : class;
    }
}
