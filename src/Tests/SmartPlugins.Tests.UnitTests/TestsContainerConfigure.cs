using SmartPlugins.Common.Abstractions.ML;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.ML.Classification;

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

        /// <summary>
        /// Get new instance the container and build of the container
        /// </summary>
        /// <returns></returns>
        public static TestsContainerConfigure GetContainer()
        {
            if (_containerConfigure == null)
            {
                _containerConfigure = new TestsContainerConfigure();
                _containerConfigure.Build();
            }

            return _containerConfigure;
        }

        /// <summary>
        /// Register defauls types
        /// </summary>
        private void RegisterTypes()
        {
            //TODO: The types will be specified here
            RegisterGenericType(typeof(ClusteringKMeans<>), typeof(IClusteringKMeans<>));
            //RegisterType<RebarNumerator, IRebarNumerator>();
            //RegisterSingleInstanceType<SmartModel, ISmartModel>();
            //RegisterSingleInstanceType<MacrosesProgressLogger, IProgressLogger>();
            //RegisterSingleInstanceType<ProgressBarViewModel, IProgressBarViewModel>();
            //RegisterSingleInstanceType<ProgressBar, IProgressWindow>();
        }
    }
}
