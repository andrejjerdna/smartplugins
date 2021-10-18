using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartExtensions
{
    public static class CollectionExtension
    {
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
        /// IEnumerable to ConcurrentBag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public static ConcurrentBag<T> ToConcurrentBag<T>(this IEnumerable<T> enumerable)
        {
            return new ConcurrentBag<T>(enumerable);
        }
    }
}
