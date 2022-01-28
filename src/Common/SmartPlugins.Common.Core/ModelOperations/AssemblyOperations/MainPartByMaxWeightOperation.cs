using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Abstractions.Pickers;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core.ModelAlgorithms;
using System.Linq;

namespace SmartPlugins.Common.Core.ModelOperations.AssemblyOperations
{
    /// <summary>
    /// Operation to change the main part by maximum weight
    /// </summary>
    public class MainPartByMaxWeightOperation : IOperation
    {
        private readonly ISmartModel _smartModel;
        private readonly IPickerObjects _pickerObjects;
        private readonly IOperationsRunner _operationsRunner;
        private readonly string _propertyName = "eee";

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="propertyName"></param>
        public MainPartByMaxWeightOperation(ISmartModel smartModel,
                                            IPickerObjects pickerObjects,
                                            IOperationsRunner operationsRunner)
        {
            _smartModel = smartModel;
            _pickerObjects = pickerObjects;
            _operationsRunner = operationsRunner;
        }

        /// <inheritdoc/>
        public void Run()
        {
            _operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesLibrary.GetAssemblies, false));

            var assemblies = _pickerObjects.GetSelectedObjectsAssembly(true).ToList();

            var totalCount = assemblies.Count;
            var count = 1;

            foreach (var assembly in assemblies)
            {
                if (assembly == null)
                    continue;

                if (_operationsRunner.CancellationToken.IsCancellationRequested)
                {
                    _operationsRunner.OperationsRunnerStop();
                    break;
                }

                assembly.SetMainPartByMaxWeight(_propertyName);

                _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

                _operationsRunner.AddOperation(() => assembly.SetMainPartByMaxWeight(_propertyName));

                 count++;
            }

            _operationsRunner.OperationsRunnerStop();
            _smartModel.CommitChanges();
        }
    }
}
