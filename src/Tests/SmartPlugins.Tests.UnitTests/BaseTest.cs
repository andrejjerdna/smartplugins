using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartPlugins.Tests.UnitTests
{
    public abstract class BaseTest
    {
        /// <summary>
        /// Container configure
        /// </summary>
        protected TestsContainerConfigure ContainerConfigure;

        /// <summary>
        /// IoC container
        /// </summary>
        protected IContainer Container;

        /// <summary>
        /// Get IoC container
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            ContainerConfigure = TestsContainerConfigure.GetContainer();

            Container = ContainerConfigure.Build();
        }
    }
}
