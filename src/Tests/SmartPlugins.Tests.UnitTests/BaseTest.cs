using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmartPlugins.Tests.UnitTests
{
    public abstract class BaseTest
    {
        /// <summary>
        /// Container configure
        /// </summary>
        protected TestsContainerConfigure ContainerConfigure;

        /// <summary>
        /// Get IoC container
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            ContainerConfigure = TestsContainerConfigure.GetContainer();
        }
    }
}
