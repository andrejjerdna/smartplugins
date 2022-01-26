using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Pickers;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Entities;

using System.Linq;

using Tekla.Structures.Model;

namespace SmartPlugins.Macros.Library.Model_macros
{
    class ObjectColorsByType : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly IPickerObjects _pickerObjects;
        private readonly IOperationsRunner _operationsRunner;

        /// <summary>
        /// .ctor
        /// </summary>
        public ObjectColorsByType(ISmartModel smartModel,
                                     IPickerObjects pickerObjects,
                                     IOperationsRunner operationsRunner)
        {
            _smartModel = smartModel;
            _pickerObjects = pickerObjects;
            _operationsRunner = operationsRunner;
        }


        public void RunLoop() => ErrorCatcher.Try(() => { throw new System.NotImplementedException(); });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            _operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesLibrary.GetParts, false));

            var parts = _pickerObjects.GetSelectedObjects<Part>(true)
                                           .Select(part => new SmartPart(part))
                                           .ToList();

            var totalCount = parts.Count;
            var count = 1;

            foreach (var assembly in parts)
            {
                if (assembly == null)
                    continue;

                var operation = new MainPartByMaxWeightOperation(parts, TeklaProperties.Weight);
                _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

                _operationsRunner.AddOperation(operation);

                count++;
            }

            _smartModel.CommitChanges();

            _operationsRunner.OperationsRunnerStop();
        }
    }
}
