using SmartPlugins.Common.Abstractions.ModelObjects;
using Tekla.Structures.Geometry3d;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public class SmartPoint : IPoint
    {
        private readonly Point _point;
        public double X { get => _point.X; set => _point.X = value; }
        public double Y { get => _point.Y; set => _point.Y = value; }
        public double Z { get => _point.Z; set => _point.Z = value; }

        public SmartPoint(Point point)
        {
            _point = point;
        }
    }
}
