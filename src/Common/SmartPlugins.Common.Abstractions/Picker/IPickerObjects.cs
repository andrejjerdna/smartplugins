using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.Picker
{
    public interface IPickerObjects
    {
        /// <summary>
        /// Get selected objects by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getAllObjects">Get all objects if no objects are selected</param>
        /// <returns></returns>
        IEnumerable<T> GetSelectedObjects<T>(bool getAllObjects);
    }
}
