using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SmartGeometry
{
    public struct Polygon
    {
        public IEnumerable<Point> points { get; set; }

        public Polygon(Point[] points)
        {
            this.points =  points.Cast<Point>().
               OrderBy(point => point.X)
                .ThenBy(point => point.Y)
                .ThenBy(point => point.Z);
        }
    }
}