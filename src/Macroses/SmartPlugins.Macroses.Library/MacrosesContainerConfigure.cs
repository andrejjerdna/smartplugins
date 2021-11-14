using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Macroses.Library.Views;

namespace SmartPlugins.Macroses.Library
{
    public class MacrosesContainerConfigure : ContainerConfigureBase
    {
        private static MacrosesContainerConfigure _container;
        
        private MacrosesContainerConfigure()
        {
            RegisterTypes();
        }

        public static MacrosesContainerConfigure GetContainer()
        {
            if (_container == null)
                _container = new MacrosesContainerConfigure();

            return _container;
        }

        /// <summary>
        /// Register types
        /// </summary>
        private void RegisterTypes()
        {
            RegisterType<ProgressState, IProgressState>();
            RegisterType<RebarNumerator, IRebarNumerator>();
            RegisterSingleInstanceType<SmartModel, ISmartModel>();
            RegisterSingleInstanceType<MacrosesProgressLogger, IProgressLogger>();
            RegisterSingleInstanceType<ProgressBarViewModel, IProgressBarViewModel>();
            RegisterSingleInstanceType<ProgressBar, IProgressWindow>();
        }
    }
}
