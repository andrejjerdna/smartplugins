using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Extensions
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Получаем перечислитель элементов модели по указанному типу.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="autoFetch"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllObjects<T>(this Model model, bool autoFetch)
        {
            ModelObjectEnumerator.AutoFetch = autoFetch;

            var types = new[] { typeof(T) };

            return model.GetModelObjectSelector()
                .GetAllObjectsWithType(types)
                .ToIEnumerable<T>();
        }
        /// <summary>
        /// IEnumerator to IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator enumerator)
        {
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T item)
                    yield return item;
            }
        }

        /// <summary>
        /// IEnumerator to ConcurrentBag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerator enumerator)
        {
            return new ConcurrentBag<T>(enumerator.ToIEnumerable<T>());
        }

        /// <summary>
        /// Polygon to IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static IEnumerable<Point> ToIEnumerable(this Polygon polygon) => polygon.Points.OfType<Point>();

        /// <summary>
        /// IEnumerable to Polygon
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static Polygon ToPolygon(this IEnumerable<Point> enumerable)
        {
            var polygon = new Polygon();
            foreach (var item in enumerable)
                polygon.Points.Add(item);
            
            return polygon;
        }

        /// <summary>
        /// Close polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static Polygon ClosePolygon(this Polygon polygon)
        {
            if (polygon.Points.Count > 0)
                polygon.Points.Add(polygon.Points[0]);

            return polygon;
        }
    }
}
