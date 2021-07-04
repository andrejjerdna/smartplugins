using System;

namespace SmartGeometry
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public Point(Tekla.Structures.Geometry3d.Point tspoint)
        {
            X = tspoint.X;
            Y = tspoint.Y;
            Z = tspoint.Z;
        }

        public static Point operator +(Point p1, Point p)
        {
            return new Point(p1.X + p.X, p1.Y + p.Y, p1.Z + p.Z);
        }
        public static Point operator -(Point p1, Point p)
        {
            return new Point(p1.X - p.X, p1.Y - p.Y, p1.Z - p.Z);
        }
        public static Point operator +(Point p1, double d)
        {
            return p1 + new Point(d, d, d);
        }

        public double Distance(Point input)
        {
            return Math.Sqrt(Math.Pow(X - input.X, 2.0) + Math.Pow(Y - input.Y, 2.0) + Math.Pow(Z - input.Z, 2.0));
        }

        public static Point Center(Point p, Point input)
        {
            Point result = p + input;
            result.X *= 0.5;
            result.Y *= 0.5;
            result.Z *= 0.5;
            return result;
        }

        public static tsg.Point toMoveVector(this tsg.Point p, tsg.Vector move)
        {
            return new tsg.Point(p.X + move.X, p.Y + move.Y, p.Z + move.Z);
        }

        public Tekla.Structures.Geometry3d.Point GetTsPoint => new Tekla.Structures.Geometry3d.Point(X, Y, Z);

        public double Length => X * X + Y * Y + Z * Z;
    }
}