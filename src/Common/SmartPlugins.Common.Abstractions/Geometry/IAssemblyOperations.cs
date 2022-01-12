using SmartPlugins.Common.Abstractions.ModelObjects;
using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.Geometry
{
    public interface IAssemblyOperations
    {
        /// <summary>
        /// Reverse part points of model object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        void SetMainPartByMaxWeight<T>(T modelObject) where T : class, IAssembly;

        /// <summary>
        /// Reverse part points of model object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        void SetMainPartByMaxWeight<T>(IEnumerable<T> modelObject) where T : class, IAssembly;
    }
}
