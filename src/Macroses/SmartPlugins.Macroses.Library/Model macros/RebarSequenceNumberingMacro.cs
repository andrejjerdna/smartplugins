using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.TeklaLibrary.Entities;
using System.Linq;
using Tekla.Structures.Model;

namespace SmartPlugins.Macroses.Library
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
            _operationsRunner.SetProgressState(new ProgressState(0, 0, MessagesEN.GetAssemblies, false));

            var reinforcements = _smartModel.GetAllObjects<Reinforcement>(true)
                                        .Select(assembly => new SmartAssembly(assembly))
                                        .ToList();

            var totalCount = reinforcements.Count;
            var count = 1;

            foreach (var assembly in reinforcements)
            {
                if (assembly == null)
                    continue;

                var operation = new MainPartByMaxWeightOperation(assembly, TeklaProperties.Weight);
                _operationsRunner.SetProgressState(new ProgressState(count, totalCount, string.Empty, false));

                _operationsRunner.AddOperation(operation);

                count++;
            }

            _smartModel.CommitChanges();

            _operationsRunner.OperationsRunnerStop();


            //_rebarNumerator.RefreshAllNumbers("REBAR_SEQ_NO");
            //MessagesViewer.Show(MessagesEN.MacroComplete, MessageType.Info);
        }
    }
}
