using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Core.ModelAlgorithms;

namespace SmartPlugins.Common.Core.ModelOperations.AssemblyOperations
{
    public sealed class NumberingSecondariesElementsOperation : IOperation
    {
        private readonly IAssembly _assembly;
        private readonly string _propertyName;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="propertyName"></param>
        public NumberingSecondariesElementsOperation(IAssembly assembly, string propertyName)
        {
            _assembly = assembly;
            _propertyName = propertyName;
        }

        /// <inheritdoc/>
        public void Run() => _assembly.NumberingSecondariesParts(_propertyName);
    }
}
