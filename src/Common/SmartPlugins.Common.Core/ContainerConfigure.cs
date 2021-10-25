using Autofac;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Container configure
    /// </summary>
    public class ContainerConfigure
    {
        private readonly ContainerBuilder _builder;

        /// <summary>
        /// .ctor
        /// </summary>
        public ContainerConfigure()
        {
            _builder = new ContainerBuilder();
        }

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterType<Class, Interface>() => _builder.RegisterType<Class>().As<Interface>();

        /// <summary>
        /// Register single instance type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterSingleInstanceType<Class, Interface>() => _builder.RegisterType<Class>().As<Interface>().SingleInstance();

        /// <summary>
        /// Create a new container with the component registrations that have been made
        /// </summary>
        /// <returns></returns>
        public IContainer Build() => _builder.Build();
    }
}
