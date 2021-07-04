using System;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace SmartGeometry
{
    public struct Circle
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public Circle(Point p, double r)
        {
            Center = p;
            Radius = r;
        }

        public Circle(Sphere sh)
        {
            Center = sh.Center;
            Radius = sh.Radius;
        }

        /// <summary>
        /// Пересечение окружностей
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double squareIntersection(Circle main, Circle input)
        {
            double D = main.Center.Distance(input.Center);
            double R1 = main.Radius;
            double R2 = input.Radius;
            
            double F1 = 2 * Math.Acos( (R1 * R1 - R2 * R2 + D * D) / (2 * R1 * D));
            double F2 = 2 * Math.Acos( (R2 * R2 - R1 * R1 + D * D) / (2 * R2 * D));

            double S1 = R1 * R1 * (F1 - Math.Sin(F1)) / 2;
            double S2 = R2 * R2 * (F2 - Math.Sin(F2)) / 2;

            return S1 + S2;
        }

        
        
        public double Square
        {
            get { return Math.PI * Radius * Radius; }
        }
}
}