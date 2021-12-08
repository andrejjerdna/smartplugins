using Autofac;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Messages;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Messages;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Rebar sequence numbering macro
    /// </summary>
    public class RebarSequenceNumberingMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void RunLoop() => ErrorCatcher.Try(() => { throw new System.NotImplementedException(); });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            var container = MacrosContainerConfigure.GetContainer().Build();

            var rebarNumerator = container.Resolve<IRebarNumerator>();
            rebarNumerator.RefreshAllNumbers("REBAR_SEQ_NO");

            MessagesViewer.Show(MessagesEN.MacroComplete, MessageType.Info);
        }
    }
}
