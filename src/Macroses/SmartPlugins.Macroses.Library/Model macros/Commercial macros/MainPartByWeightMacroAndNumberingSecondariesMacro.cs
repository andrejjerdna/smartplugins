using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Entities;
using System.Linq;
using Tekla.Structures.Model;

namespace SmartPlugins.Macroses.Library
{
    public class MainPartByWeightMacroAndNumberingSecondariesMacro : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly IOperationsRunner _operationsRunner;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="mainPartByWeight"></param>
        public MainPartByWeightMacroAndNumberingSecondariesMacro(ISmartModel smartModel, IOperationsRunner operationsRunner)
        {
            _smartModel = smartModel;
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
            _operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesEN.GetAssemblies, false));

            var assemblies = _smartModel.GetAllObjects<Assembly>(true)
                                        .Select(assembly => new SmartAssembly(assembly))
                                        .ToList();

            var totalCount = assemblies.Count;
            var count = 0;

            foreach (var assembly in assemblies)
            {
                if (assembly == null)
                    continue;

                var operation1 = new MainPartByMaxWeightOperation(assembly, TeklaProperties.Weight);
                var operation2 = new NumberingSecondariesElementsOperation(assembly, TeklaProperties.Weight);
                _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

                _operationsRunner.AddOperation(operation1);
                _operationsRunner.AddOperation(operation2);

                count++;
            }

            _smartModel.CommitChanges();

            _operationsRunner.OperationsRunnerStop();
        }
    }
}
