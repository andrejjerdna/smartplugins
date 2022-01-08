using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary.Extensions;
using System.Collections.Generic;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary
{
    /// <summary>
    /// Picker for tekla model objects
    /// </summary>
    public class SmartPicker : ISmartPicker
    {
        private readonly Picker _picker;

        /// <summary>
        /// .ctor
        /// </summary>
        public SmartPicker()
        {
            _picker = new Picker();
        }

        /// <inheritdoc/>
        public T PickObject<T>(int parameter) where T : class
        {
            var pickObjectEnum = (Picker.PickObjectEnum)parameter;

            return _picker.PickObject(pickObjectEnum) as T;
        }

        /// <inheritdoc/>
        public IEnumerable<T> PickObjects<T>(int parameter, string message) where T : class
        {
            var pickObjectEnum = (Picker.PickObjectsEnum)parameter;

            return _picker.PickObjects(pickObjectEnum, message).ToIEnumerable<T>();
        }
    }
}
