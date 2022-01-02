using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Core.ModelAlgorithms;

namespace SmartPlugins.Common.Core.ModelOperations.AssemblyOperations
{
    /// <summary>
    /// Operation to change the main part by maximum weight
    /// </summary>
    public class MainPartByMaxWeightOperation : IOperation
    {
        private readonly IAssembly _assembly;
        private readonly string _propertyName;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="propertyName"></param>
        public MainPartByMaxWeightOperation(IAssembly assembly, string propertyName)
        {
            _assembly = assembly;
            _propertyName = propertyName;
        }

        /// <inheritdoc/>
        public void Run() => _assembly.SetMainPartByMaxWeight(_propertyName);
    }
}
