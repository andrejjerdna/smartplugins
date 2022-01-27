using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Pickers;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Entities;
using System.Linq;
using Tekla.Structures.Model;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Main part by max weight in assembly
    /// </summary>
    public class MainPartByWeightMacro : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly IPickerObjects _pickerObjects;
        private readonly IOperationsRunner _operationsRunner;

        /// <summary>
        /// .ctor
        /// </summary>
        public MainPartByWeightMacro(ISmartModel smartModel,
                                     IPickerObjects pickerObjects,
                                     IOperationsRunner operationsRunner)
        {
            _smartModel = smartModel;
            _pickerObjects = pickerObjects;
            _operationsRunner = operationsRunner;
        }

        /// <inheritdoc/>
        public void RunLoop() => ErrorCatcher.Try(() => { throw new System.NotImplementedException(); });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            var operation = new MainPartByMaxWeightOperation(_smartModel, _pickerObjects, _operationsRunner, TeklaProperties.Weight);
            operation.Run();
        }
    }
}
