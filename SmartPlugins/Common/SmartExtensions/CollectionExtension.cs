using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;

namespace SmartExtensions
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
        public static IEnumerable<T> GetAllParts<T>(this tsm.Model model, bool autoFetch)
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
                {
                    yield return item;
                }
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
    }
}
