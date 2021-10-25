using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.TeklaLibrary;

namespace SmartPlugins.Macroses.Library
{
    public class MacrosesContainerConfigure : ContainerConfigure
    {
        public MacrosesContainerConfigure()
        {
            RegisterLocalTypes();
        }

        private void RegisterLocalTypes()
        {
            RegisterType<ProgressState, IProgressState>();
            RegisterType<RebarNumerator, IRebarNumerator>();
        }
    }
}
