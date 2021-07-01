﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartExtensions.Geometry
{
    public static class PointExtensions
    {
        /* Актуально при своих классах
        public static ACD.Library.Geometry.Point GetACDPoint(this Point p)
        {
            return new Geometry.Point(p.X, p.Y, p.Z);
        }
        */
        
        /// <summary>
        /// Получение COG детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point GetCOG(this Part p)
        {
            return p.GetAabb().GetCenterPoint();
        }

        /// <summary>
        /// Получить все точки детали
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IEnumerable<Point> GetPoints(this Part p)
        {
            return p.GetCenterLine(true).ToList().AsEnumerable();
        }

        /// <summary>
        /// Проверка что точка лежит между двумя другими
        /// </summary>
        /// <param name="point"></param>
        /// <param name="minPoint"></param>
        /// <param name="maxPoint"></param>
        /// <returns></returns>
        public static bool IsInside(this Point point, Point minPoint, Point maxPoint)
        {
            var x = point.X > minPoint.X && point.X < maxPoint.X;
            var y = point.Y > minPoint.Y && point.Y < maxPoint.Y;
            var z = point.Z > minPoint.Z && point.Z < maxPoint.Z;

            return x && y && z;
        }

        /// <summary>
        /// Центральная точка
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point GetCenterPoint(this Point p1, Point p2)
        {
            return new Point
            {
                X = (p2.X + p1.X) * 0.5,
                Y = (p2.Y + p1.Y) * 0.5,
                Z = (p2.Z + p1.Z) * 0.5
            };
        }

        /// <summary>
        /// Получить минимальную\максимальную точки
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static void MinMax(ref Point min, ref Point max)
        {
            var p1 = min.Min(max);
            var p2 = max.Max(min);

            min = new Point(p1);
            max = new Point(p2);
        }

        /// <summary>
        /// Получить минимальную точку
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point Min(this Point a, Point b)
        {
            return new Point
            {
                X = Math.Min(a.X, b.X),
                Y = Math.Min(a.Y, b.Y),
                Z = Math.Min(a.Z, b.Z),
            };
        }

        /// <summary>
        /// Получить максимальную точку
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Point Max(this Point a, Point b)
        {
            return new Point
            {
                X = Math.Max(a.X, b.X),
                Y = Math.Max(a.Y, b.Y),
                Z = Math.Max(a.Z, b.Z),
            };
        }

        /// <summary>
        /// Получить минимальную точку 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Point Min(this AABB a)
        {
            return new Point
            {
                X = Math.Min(a.MinPoint.X, a.MaxPoint.X),
                Y = Math.Min(a.MinPoint.Y, a.MaxPoint.Y),
                Z = Math.Min(a.MinPoint.Z, a.MaxPoint.Z),
            };
        }

        /// <summary>
        /// Получить максимальную точку
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Point Max(this AABB a)
        {
            return new Point
            {
                X = Math.Max(a.MinPoint.X, a.MaxPoint.X),
                Y = Math.Max(a.MinPoint.Y, a.MaxPoint.Y),
                Z = Math.Max(a.MinPoint.Z, a.MaxPoint.Z),
            };
        }

        /// <summary>
        /// Получить точки в виде списка
        /// </summary>
        /// <param name="bg"></param>
        /// <returns></returns>
        public static List<Point> GetStartEndPoints(this BoltGroup bg)
        {
            return new List<Point>()
            {
                bg.FirstPosition,
                bg.SecondPosition
            };
        }

        /// <summary>
        /// Перевод точек из ArrayList в List
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static List<Point> ToList(this ArrayList position)
        {
            List<Point> points = new List<Point>();
            foreach (var k in position)
            {
                points.Add(k as Point);
            }

            return points;
        }
        /// <summary>
        /// Перевод точек из ArrayList в List
        /// с дополнительной трансформацией
        /// </summary>
        /// <param name="position"></param>
        /// <param name="vMatrix"></param>
        /// <returns></returns>
        public static List<Point> ToList(this ArrayList position, Matrix vMatrix)
        {
            List<Point> points = new List<Point>();
            foreach (var k in position)
            {
                points.Add(vMatrix.Transform(k as Point));
            }

            return points;
        }
        /// <summary>
        /// Получить центральную точку из ArrayList точек
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetCog(this ArrayList positions)
        {
            return positions.GetMinPoint().GetCenterPoint(positions.GetMaxPoint());
        }
        /// <summary>
        /// Получить центральную точку из List точек
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetCog(this List<Point> positions)
        {
            return positions.GetMinPoint().GetCenterPoint(positions.GetMaxPoint());
        }

        /// <summary>
        /// Получить минимальную точку из ArrayList
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMinPoint(this ArrayList positions)
        {
            var min = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                min = (k as Point).Min(min);
            }

            return min;
        }
        /// <summary>
        /// Получить минимальную точку из List
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMinPoint(this List<Point> positions)
        {
            var min = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                min = (k as Point).Min(min);
            }

            return min;
        }

        /// <summary>
        /// Получить максимальную точку из ArrayList
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMaxPoint(this List<Point> positions)
        {
            var max = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                max = (k as Point).Max(max);
            }

            return max;
        }
        /// <summary>
        /// Получить минимальную точку из List
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static Point GetMaxPoint(this ArrayList positions)
        {
            var max = new Point(positions[0] as Point);
            foreach (var k in positions)
            {
                max = (k as Point).Max(max);
            }

            return max;
        }

        /// <summary>
        /// Отнять от всех координат точки значение
        /// </summary>
        /// <param name="p"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static Point MinusDistance(this Point p, double dist)
        {
            return new Point(p.X - dist, p.Y - dist, p.Z - dist);
        }

        /// <summary>
        /// Прибавить ко всем координатам точки значение
        /// </summary>
        /// <param name="p"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static Point PlusDistance(this Point p, double dist)
        {
            return p.MinusDistance(-1 * dist);
        }
    }
}
