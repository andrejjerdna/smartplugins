using SmartPlugins.Common.Core;

namespace SmartPlugins.Tests.UnitTests
{
    /// <summary>
    /// IoC container for tests
    /// </summary>
    public sealed class TestsContainerConfigure : ContainerConfigureBase
    {
        private static TestsContainerConfigure _containerConfigure;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <returns></returns>
        private TestsContainerConfigure()
        {
            RegisterTypes();
        }

        public static TestsContainerConfigure GetContainer()
        {
            if (_containerConfigure == null)
                _containerConfigure = new TestsContainerConfigure();

            return _containerConfigure;
        }

        /// <summary>
        /// Register defauls types
        /// </summary>
        private void RegisterTypes()
        {
            //TODO: The types will be specified here
            //RegisterType<ProgressState, IProgressState>();
            //RegisterType<RebarNumerator, IRebarNumerator>();
            //RegisterSingleInstanceType<SmartModel, ISmartModel>();
            //RegisterSingleInstanceType<MacrosesProgressLogger, IProgressLogger>();
            //RegisterSingleInstanceType<ProgressBarViewModel, IProgressBarViewModel>();
            //RegisterSingleInstanceType<ProgressBar, IProgressWindow>();
        }
    }
}
