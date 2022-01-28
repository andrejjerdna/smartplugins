using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using SmartPlugins.Common.TeklaLibrary.Entities;
using System.Linq;
using Tekla.Structures.Model;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Rebar sequence numbering macro
    /// </summary>
    public class RebarSequenceNumberingMacro : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly IOperationsRunner _operationsRunner;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="operationsRunner"></param>
        public RebarSequenceNumberingMacro(ISmartModel smartModel, IOperationsRunner operationsRunner)
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
            _operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesLibrary.GetAssemblies, false));

            var castUnits = _smartModel.GetAllObjects<Assembly>(true)
                                       .Select(assembly => new SmartCastUnit(assembly))
                                       .ToList();

            var totalCount = castUnits.Count;
            var count = 1;

            foreach (var castUnit in castUnits)
            {
                if (castUnit == null)
                    continue;

                var operation = new RebarSequenceNumberingOperation(castUnit);
                _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

               // _operationsRunner.AddOperation(operation);

                count++;
            }

            _smartModel.CommitChanges();

            _operationsRunner.OperationsRunnerStop();
        }
    }
}
