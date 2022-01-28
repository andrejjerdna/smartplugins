using SmartPlugins.Common.Abstractions;

namespace SmartPlugins.Common.Core
{
    public class OperationLauncher
    {
        private readonly ContainerConfigureBase _container;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="container"></param>
        public OperationLauncher(ContainerConfigureBase container)
        {
            _container = container;
        }

        /// <summary>
        /// Macro in once run mode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Run<T>() where T : class, IOperation
        {
            _container.GetRequiredService<T>().Run();
        }
    }
}
