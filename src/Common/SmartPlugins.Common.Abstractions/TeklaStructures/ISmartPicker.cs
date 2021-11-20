using System;
using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.TeklaStructures
{
    public interface ISmartPicker
    {
        /// <summary>
        /// Pick one object by user input type
        /// </summary>
        /// <param name="pickObjectEnum"></param>
        /// <returns></returns>
        T PickObject<T>(int parameter) where T : class;

        /// <summary>
        /// Pick n-object by user input type
        /// </summary>
        /// <param name="pickObjectEnum"></param>
        /// <returns></returns>
        IEnumerable<T> PickObjects<T>(int parameter, string message) where T : class;
    }
}
