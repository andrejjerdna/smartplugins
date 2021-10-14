using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace SmartMacros
{
    public class DrawObjectCoordinateSystem
    {
        public void Run()
        {
              DrawCoordinateSystem();
        }

        /// <summary>
        /// Позволяет выбрать в модели объект и русует его систему координат.
        /// </summary>
        private static void DrawCoordinateSystem()
        {
            var model = new Model();
            if (!model.GetConnectionStatus()) return;

            var workPlaneHandler = model.GetWorkPlaneHandler();
            var originalTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();

            var picker = new Picker();
            var modelObject = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT);

            if (modelObject == null) return;

            if (modelObject is Part part)
            {
                var partCoordinateSystem = part.GetCoordinateSystem();
                DrawPlane(partCoordinateSystem);
            }

            if (modelObject is BoltGroup boltGroup)
            {
                var partCoordinateSystem = boltGroup.GetCoordinateSystem();
                DrawPlane(partCoordinateSystem);
            }

            if (modelObject is Weld weld)
            {
                var partCoordinateSystem = weld.GetCoordinateSystem();
                DrawPlane(partCoordinateSystem);
            }

            if (modelObject is RebarGroup rebarGroup)
            {
                var partCoordinateSystem = rebarGroup.GetCoordinateSystem();
                DrawPlane(partCoordinateSystem);
            }

            workPlaneHandler.SetCurrentTransformationPlane(originalTransformationPlane);
        }

        private static void DrawPlane(CoordinateSystem planeCoord)
        {
            Color lineColor = new Color(1.0, 0.0, 0.0);
            Color textColor = new Color(0.0, 0.0, 1.0);
            Color textColor2 = new Color(0.0, 0.0, 0.0);

            Vector vector = planeCoord.AxisX.Cross(planeCoord.AxisY);
            planeCoord.AxisX.Normalize(1500.0);
            planeCoord.AxisY.Normalize(1500.0);
            vector.Normalize(1500.0);
            GraphicsDrawer graphicsDrawer = new GraphicsDrawer();
            Point point = new Point(planeCoord.Origin + planeCoord.AxisX);
            Point point2 = new Point(planeCoord.Origin + planeCoord.AxisY);
            Point point3 = new Point(planeCoord.Origin + vector);
            graphicsDrawer.DrawLineSegment(planeCoord.Origin, point, lineColor);
            graphicsDrawer.DrawLineSegment(planeCoord.Origin, point2, lineColor);
            graphicsDrawer.DrawLineSegment(planeCoord.Origin, point3, lineColor);
            graphicsDrawer.DrawText(point, "X", textColor);
            graphicsDrawer.DrawText(point2, "Y", textColor);
            graphicsDrawer.DrawText(point3, "Z", textColor);
            graphicsDrawer.DrawText(planeCoord.Origin, planeCoord.Origin.ToString(), textColor2);
        }
    }
}
