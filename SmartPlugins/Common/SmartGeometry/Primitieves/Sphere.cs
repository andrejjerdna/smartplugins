using System;
using System.Data.Common;

namespace SmartGeometry
{
    public struct Sphere
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public Sphere(Point center, double radius)
        {
            Center = new Point(center);
            Radius = radius;
        }
        public Sphere(Sphere input)
        {
            Center = new Point(input.Center);
            Radius = input.Radius;
        }

        public double DistanceToOtherSphere(Point input) => Center.Distance(input);

        public double DistanceToOtherSphere(Sphere sh) => Center.Distance(sh.Center);

        public static Sphere operator +(Sphere sh, Point p) => new Sphere(sh.Center + p, sh.Radius);

        public static Sphere operator +(Sphere sh, double radius) => new Sphere(sh.Center, sh.Radius + radius);
        
        
        #region Is

        public bool IsInsidePoint(Point input)
        {
            return (Center - input).Length <= Radius * Radius;
        }

        /// <summary>
        /// Проверка пересечения сфер
        /// </summary>
        /// <param name="sh"></param>
        /// <returns></returns>
        public bool IsIntersectionSphere(Sphere sh)
        {
            double dist = DistanceToOtherSphere(sh);
            double sum = Radius + sh.Radius;
            double diff = Math.Abs(Radius - sh.Radius);
            
            bool inside = diff <= dist && dist <= sum;

            return inside;
        }

        public bool IsContact(Sphere sh)
        {
            return IsInside(sh) || IsIntersectionSphere(sh);
        }

        public bool IsInside(Sphere sh)
        {
            if (IsInsidePoint(sh.Center))
            {
                double dist = DistanceToOtherSphere(sh);
                double diff = Math.Abs(Radius - sh.Radius);
                return dist < diff;
            }

            return false;
        }
        
        
        
        #endregion
    }
}