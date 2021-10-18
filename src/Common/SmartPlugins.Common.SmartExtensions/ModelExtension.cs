using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace SmartExtensions
{
    public static class ModelExtension
    {
        /// <summary>
        /// Получаем перечислитель элементов модели по указанному типу.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="autoFetch"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllParts<T>(this Model model, bool autoFetch)
        {
            ModelObjectEnumerator.AutoFetch = autoFetch;

            var types = new[] { typeof(T) };

            return model.GetModelObjectSelector()
                .GetAllObjectsWithType(types)
                .ToIEnumerable<T>();
        }

        public static IEnumerable<T> GetSelectedObjects<T>(this Model model)
            where T : ModelObject
        {
            return new ModelObjectSelector().GetSelectedObjects().ToIEnumerable<T>();
        }
    }
}
