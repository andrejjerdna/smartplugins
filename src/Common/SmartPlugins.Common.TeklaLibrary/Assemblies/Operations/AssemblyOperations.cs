using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using System.Collections.Generic;

namespace SmartPlugins.Common.TeklaLibrary.Assemblies
{
    /// <summary>
    /// Operations for tekla assembly
    /// </summary>
    public sealed class AssemblyOperations : IAssemblyOperations
    {
        private readonly IProgressLogger _progressLogger;

        public AssemblyOperations(IProgressLogger progressLogger)
        {
            _progressLogger = progressLogger;
        }

        public void SetMainPartByMaxWeight<T>(IEnumerable<T> modelObjects) where T : class, IAssembly
        {
           // new MainPartByWeight(_progressLogger).CheckAllAssemblies(modelObjects, TeklaProperties.Weight);
        }

        public void SetMainPartByMaxWeight<T>(T modelObject) where T : class, IAssembly
        {
            //new MainPartByWeight(_progressLogger).CheckAssembly(modelObject, TeklaProperties.Weight);
        }
    }
}
