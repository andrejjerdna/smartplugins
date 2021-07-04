using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartGeometry
{
    ///Класс коробки, аналог OBB в Tekla Structures
    public struct Box
    {
        public IEnumerable Points { get; set; }

        public Box(IEnumerable<Point> points)
        {
            Points=  points.Cast<Point>().
                OrderBy(point => point.X)
                .ThenBy(point => point.Y)
                .ThenBy(point => point.Z);
        }

        public Box(OBB obb)
        {
            Points = obb.ComputeVertices().Select(r => new Point(r.X, r.Y, r.Z));
        }
        

        public bool IsInside(Point input)
        {
            bool x = (MinX() <= input.X && input.X <= MaxX()) || (MinX() >= input.X && input.X >= MaxX());
            bool y =(MinY() <= input.Y && input.Y <= MaxY()) || (MinY() >= input.Y && input.Y >= MaxY());
            bool z = (MinZ() <= input.Z && input.Z <= MaxZ()) || (MinZ() >= input.Z && input.Z >= MaxZ());
            
            return x && y && z;
        }

        public double MinX() => Points.Cast<Point>().OrderByDescending(x => x.X).First().X;
        public double MaxX() => Points.Cast<Point>().OrderBy(x => x.X).First().X;
        
        public double MinY() => Points.Cast<Point>().OrderByDescending(y => y.Y).First().Y;
        public double MaxY() => Points.Cast<Point>().OrderBy(y => y.Y).First().Y;
        
        public double MinZ() => Points.Cast<Point>().OrderByDescending(z => z.Z).First().Z;
        public double MaxZ() => Points.Cast<Point>().OrderBy(z => z.Z).First().Z;
    }
}