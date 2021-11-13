using SmartPlugins.Common.Abstractions.TeklaStructures;
using System;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

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

        /// <summary>
        /// Pick one object by user input type
        /// </summary>
        /// <param name="pickObjectEnum"></param>
        /// <returns></returns>
        public T PickObject<T>(Picker.PickObjectEnum pickObjectEnum)
            where T : ModelObject
        {
            return _picker.PickObject(pickObjectEnum) as T;
        }
    }
}
