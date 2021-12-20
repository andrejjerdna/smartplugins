using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Messages;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Messages;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Main part by max weight in assembly
    /// </summary>
    public class MainPartByWeightMacro : ITeklaMacro
    {
        private readonly IMainPartByWeight _mainPartByWeight;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="mainPartByWeight"></param>
        public MainPartByWeightMacro(IMainPartByWeight mainPartByWeight)
        {
            _mainPartByWeight = mainPartByWeight;
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
            _mainPartByWeight.CheckAllAssemblies();
            MessagesViewer.Show(MessagesEN.MacroComplete, MessageType.Info);
        }
    }
}
