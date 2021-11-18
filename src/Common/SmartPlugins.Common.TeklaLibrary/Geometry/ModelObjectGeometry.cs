using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Geometry
{
    public class ModelObjectGeometry
    {
        private Solid _solid { get; }
        /// <summary>
        /// Увеличивает коробку OBB заданную величину равномерно во все стороны.
        /// </summary>
        private double _delta { get; }
        private ModelObject _modelObject { get; }
        private tsm.Model _model { get; }

        public double MinX { get => _solid.MinimumPoint.X; }
        public double MinY { get => _solid.MinimumPoint.Y; }
        public double MinZ { get => _solid.MinimumPoint.Z; }
        public double MaxX { get => _solid.MaximumPoint.X; }
        public double MaxY { get => _solid.MaximumPoint.Y; }
        public double MaxZ { get => _solid.MaximumPoint.Z; }
        public double MidX { get => (MinX + MaxX) / 2; }
        public double MidY { get => (MinY + MaxY) / 2; }
        public double MidZ { get => (MinZ + MaxZ) / 2; }
        public Point MinPoint { get => _solid.MinimumPoint; }
        public Point MaxPoint { get => _solid.MinimumPoint; }

        public ModelObjectGeometry(ModelObject currentModelObject)
        {
            _model = new tsm.Model();
            _modelObject = currentModelObject;
            _solid = CreateSolid(_modelObject);
            _delta = 0.0;
        }

        public ModelObjectGeometry(tsm.Model currentModel, ModelObject currentModelObject)
        {
            _model = currentModel;
            _modelObject = currentModelObject;
            _solid = CreateSolid(_modelObject);
            _delta = 0.0;
        }

        public ModelObjectGeometry(tsm.Model currentModel, ModelObject currentModelObject, double deltaUser)
        {
            _model = currentModel;
            _modelObject = currentModelObject;
            _solid = CreateSolid(_modelObject);
            _delta = deltaUser;
        }

        public OBB GetOBB()
        {
            return CreateOrientedBoundingBox(_modelObject, _solid, _model, _delta);
        }

        public AABB GetABB()
        {
            return new AABB(MinPoint, MaxPoint);
        }

        public Solid GetSolid()
        {
            return _solid;
        }

        /// <summary>
        /// Метод получения "коробки" (OBB).
        /// </summary>
        /// <param name="modelObject">Объект модели.</param>
        /// <param name="model">Модель.</param>
        /// <param name="delta">Дельта для равномерного увеличения коробки OBB.</param>
        /// <returns></returns>
        private OBB CreateOrientedBoundingBox(ModelObject modelObject, Solid solid, tsm.Model model, double delta)
        {
            OBB obb = null;

            if (modelObject != null)
            {
                var workPlaneHandler = model.GetWorkPlaneHandler();
                var originalTransformationPlane = workPlaneHandler.GetCurrentTransformationPlane();

                if (solid != null)
                {
                    var minPointInCurrentPlane = solid.MinimumPoint;
                    var maxPointInCurrentPlane = solid.MaximumPoint;

                    var centerPoint = CalculateCenterPoint(minPointInCurrentPlane, maxPointInCurrentPlane);

                    var coordSys = modelObject.GetCoordinateSystem();
                    var localTransformationPlane = new TransformationPlane(coordSys);
                    workPlaneHandler.SetCurrentTransformationPlane(localTransformationPlane);

                    var minPoint = new Point(solid.MinimumPoint.X - delta, solid.MinimumPoint.Y - delta, solid.MinimumPoint.Z - delta);
                    var maxPoint = new Point(solid.MaximumPoint.X + delta, solid.MaximumPoint.Y + delta, solid.MaximumPoint.Z + delta);
                    var extent0 = (maxPoint.X - minPoint.X) / 2;
                    var extent1 = (maxPoint.Y - minPoint.Y) / 2;
                    var extent2 = (maxPoint.Z - minPoint.Z) / 2;

                    workPlaneHandler.SetCurrentTransformationPlane(originalTransformationPlane);

                    obb = new OBB(centerPoint, coordSys.AxisX, coordSys.AxisY,
                                    coordSys.AxisX.Cross(coordSys.AxisY), extent0, extent1, extent2);
                }
            }

            return obb;
        }

        /// <summary>
        /// Вычисление центральной точки OBB.
        /// </summary>
        /// <param name="min">Point min</param>
        /// <param name="max">Point max</param>
        /// <returns></returns>
        private Point CalculateCenterPoint(Point min, Point max)
        {
            double x = min.X + ((max.X - min.X) / 2);
            double y = min.Y + ((max.Y - min.Y) / 2);
            double z = min.Z + ((max.Z - min.Z) / 2);

            return new Point(x, y, z);
        }
        /// <summary>
        /// Получаем Solid.
        /// </summary>
        /// <param name="modelObject">Объект модели.</param>
        /// <returns></returns>
        private Solid CreateSolid(ModelObject modelObject)
        {
            if (modelObject is Part part)
            {
                return part.GetSolid();
            }

            if (modelObject is BoltGroup boltGroup)
            {
                return boltGroup.GetSolid();
            }

            if (modelObject is BaseWeld baseWeld)
            {
                return baseWeld.GetSolid();
            }

            return null;
        }
    }
}

