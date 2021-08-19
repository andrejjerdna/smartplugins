using Ninject;
using SmartPlugins.Applications.SmartHelper.Pages.TestApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Applications.SmartHelper
{
    public static class Container
    {
        private static IKernel _container;

        static Container()
        {
            _container = new StandardKernel();
            _container.Bind<ITestAppRunner>().To<TestAppRunner>().InTransientScope();
        }
    }
}
