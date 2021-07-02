/* System */
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
/* Tekla Structures */
using Tekla.Structures.Model;
using tsm = Tekla.Structures.Model;
using ts = Tekla.Structures;
using tsd = Tekla.Structures.Drawing;

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
        /// Получает объекты модели в IEnumerable из ModelObjectEnumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moe"></param>
        /// <returns></returns>
        public static IEnumerable<T> Get<T>(this tsm.ModelObjectEnumerator moe)
        {
            foreach (var item in moe)
            {
                if (item is T result)
                    yield return result;
            }
        }

        /// <summary>
        /// Получает объекты чертежа в IEnumerable из DrawingObjectEnumerator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doe"></param>
        /// <returns></returns>
        public static IEnumerable<T> Get<T>(this tsd.DrawingObjectEnumerator doe)
        {
            foreach (var item in doe)
            {
                if (item is T result)
                    yield return result;
            }
        }

        /// <summary>
        /// Выбрать объект по ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T ToModel<T>(this ts.Identifier id)
        {
            var obj = new tsm.Model().SelectModelObject(id);
            return (T)(object)obj;
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
    }
}
