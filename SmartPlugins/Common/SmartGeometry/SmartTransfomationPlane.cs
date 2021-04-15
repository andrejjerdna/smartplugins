using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartGeometry
{
    public class SmartTransfomationPlane
    {
        /// <summary>
        /// Ориентация системы координат по двум точкам.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static TransformationPlane GetTransformationPlaneTwoPoints(Model model, Point point1, Point point2)
        {
            var currentTP = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var baseTP = new TransformationPlane();

            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());

            point1 = baseTP.TransformationMatrixToLocal.Transform(currentTP.TransformationMatrixToGlobal.Transform(point1));
            point2 = baseTP.TransformationMatrixToLocal.Transform(currentTP.TransformationMatrixToGlobal.Transform(point2));

            var vectorX = new Vector(point2 - point1);

            var workCS = new CoordinateSystem
            {
                Origin = point1,
                AxisX = vectorX,
                AxisY = vectorX.Cross(new Vector(0, 0, -1))
            };

            var workPlane = new TransformationPlane(workCS);

            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

            return workPlane;
        }
    }
}
