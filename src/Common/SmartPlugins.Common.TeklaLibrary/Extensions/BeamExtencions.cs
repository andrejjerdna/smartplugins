using SmartPlugins.Common.TeklaLibrary.Geometry;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class BeamExtencions
    {
        public static IEnumerable<Point> GetAllPointsProfile(this Beam beam, Model model)
        {
            var currentTP = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var beamTP = new TransformationPlane(beam.GetCoordinateSystem());
            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(beamTP);

            var beamCenterPoint = beam.GetCenterPoint();

            var polygon = IntersectWithSolid.OutmostPoints(beam, 
                                                           new Point(beamCenterPoint.X, beamCenterPoint.Y, beamCenterPoint.Z), 
                                                           new Point(beamCenterPoint.X, beamCenterPoint.Y + 1000, beamCenterPoint.Z), 
                                                           new Point(beamCenterPoint.X, beamCenterPoint.Y, beamCenterPoint.Z + 1000), 
                                                           Solid.SolidCreationTypeEnum.HIGH_ACCURACY)
                .Select(p => p.TransformationPoint(beamTP, currentTP));

            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);

            return polygon;
        }

        public static Point GetCenterPoint(this Beam beam, bool withCutsFirrings = false)
        {
            var centerline = beam.GetCenterLine(withCutsFirrings).OfType<Point>();
            return CSLib.Geo.GetCenterPoint3D(centerline.First(), centerline.Last());
        }

        public static Point IntersectCenterLineWithSolid(this Beam beam, Model model, Solid solid)
        {
            if (solid == null)
                return null;

            var currentTP = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            var beamTP = new TransformationPlane(beam.GetCoordinateSystem());
            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(beamTP);

            var points = beam.GetAllPointsProfile(model);

            var centerline = beam.GetCenterLine(false).OfType<Point>();
            var centerPoint = beam.GetCenterPoint();

            if(solid.)

            solid.Intersect(new LineSegment(centerline.First(), centerline.Last()));

            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(currentTP);
        }
    }
}
