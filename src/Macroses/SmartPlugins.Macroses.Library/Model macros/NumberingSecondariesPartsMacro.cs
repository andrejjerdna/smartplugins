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
    public class NumberingSecondariesPartsMacro : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly IPickerObjects _pickerObjects;
        private readonly IOperationsRunner _operationsRunner;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="mainPartByWeight"></param>
        public NumberingSecondariesPartsMacro(ISmartModel smartModel, 
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
            //_operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesLibrary.GetAssemblies, false));

            //var assemblies = _pickerObjects.GetSelectedObjectsAssembly<Assembly>(true)
            //                               .Select(assembly => new SmartAssembly(assembly))
            //                               .ToList();

            //var totalCount = assemblies.Count;
            //var count = 1;

            //foreach(var assembly in assemblies)
            //{
            //    if (assembly == null)
            //        continue;

            //    var operation = new NumberingSecondariesElementsOperation(assembly, TeklaProperties.Weight);
            //    _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

            //    //_operationsRunner.AddOperation(operation);

            //    count++;
            //}

            //_smartModel.CommitChanges();

            //_operationsRunner.OperationsRunnerStop();
        }
    }
}
