using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;

namespace SmartPlugins.Common.TeklaLibrary
{
    /// <summary>
    /// Draw objects in tekla model
    /// </summary>
    public class DrawInTeklaModel
    {
        /// <summary>
        /// Draw tekla object coordinate system
        /// </summary>
        /// <param name="objectCoordinateSystem">Object coordinate system</param>
        public static void DrawTeklaObjectCoordinateSystem(CoordinateSystem objectCoordinateSystem)
        {
            var lineColor = new Color(1.0, 0.0, 0.0);
            var textColor = new Color(0.0, 0.0, 1.0);
            var textColor2 = new Color(0.0, 0.0, 0.0);

            var vector = objectCoordinateSystem.AxisX.Cross(objectCoordinateSystem.AxisY);
            objectCoordinateSystem.AxisX.Normalize(1500.0);
            objectCoordinateSystem.AxisY.Normalize(1500.0);
            vector.Normalize(1500.0);

            var graphicsDrawer = new GraphicsDrawer();

            var point = new Point(objectCoordinateSystem.Origin + objectCoordinateSystem.AxisX);
            var point2 = new Point(objectCoordinateSystem.Origin + objectCoordinateSystem.AxisY);
            var point3 = new Point(objectCoordinateSystem.Origin + vector);

            graphicsDrawer.DrawLineSegment(objectCoordinateSystem.Origin, point, lineColor);
            graphicsDrawer.DrawLineSegment(objectCoordinateSystem.Origin, point2, lineColor);
            graphicsDrawer.DrawLineSegment(objectCoordinateSystem.Origin, point3, lineColor);
            graphicsDrawer.DrawText(point, "X", textColor);
            graphicsDrawer.DrawText(point2, "Y", textColor);
            graphicsDrawer.DrawText(point3, "Z", textColor);
            graphicsDrawer.DrawText(objectCoordinateSystem.Origin, objectCoordinateSystem.Origin.ToString(), textColor2);
        }
    }
}
