using Autofac;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Container configure
    /// </summary>
    public abstract class ContainerConfigureBase
    {
        private IContainer _container;

        protected readonly ContainerBuilder Builder;

        /// <summary>
        /// .ctor
        /// </summary>
        public ContainerConfigureBase()
        {
            Builder = new ContainerBuilder();
        }

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterType<Class, Interface>() => Builder.RegisterType<Class>().As<Interface>();

        /// <summary>
        /// Register single instance type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterSingleInstanceType<Class, Interface>() => Builder.RegisterType<Class>().As<Interface>().SingleInstance();

        /// <summary>
        /// Create a new container with the component registrations that have been made
        /// </summary>
        /// <returns></returns>
        public IContainer Build()
        {
            if (_container == null)
                _container = Builder.Build();

            return _container;
        }
    }
}
