using SmartPlugins.Common.Abstractions.TeklaStructures;
using System;
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
        public T1 PickObject<T1, T2>(string parameter) where T1 : class where T2 : Enum
        {
            Picker.PickObjectEnum pickObjectEnum;

            var value = Enum.TryParse(parameter, out pickObjectEnum);

            if(value == false)
                return null;

            return _picker.PickObject(pickObjectEnum) as T1;
        }
    }
}
