using Microsoft.Extensions.DependencyInjection;
using SmartPlugins.Common.Core.Exceptions;
using System;

namespace SmartPlugins.Common.Core
{
    /// <summary>
    /// Container configure
    /// </summary>
    public abstract class ContainerConfigureBase
    {
        private IServiceProvider _container;

        protected readonly ServiceCollection Builder;

        /// <summary>
        /// .ctor
        /// </summary>
        public ContainerConfigureBase()
        {
            Builder = new ServiceCollection();
        }

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterType<Class, Interface>() where Interface : class where Class : class, Interface => Builder.AddScoped<Interface, Class>();

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        public void RegisterType<Class>() where Class : class => Builder.AddScoped<Class>();

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterGenericType(Type classType, Type interfaceType) => Builder.AddScoped(interfaceType, classType);

        /// <summary>
        /// Register single instance type
        /// </summary>
        /// <typeparam name="Class"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterSingleInstanceType<Class, Interface>() where Interface : class where Class : class, Interface => Builder.AddSingleton<Interface, Class>();

        /// <summary>
        /// Get service of type T from the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRequiredService<T>()
        {
            if (_container == null)
                throw new ContainerBuildExeption();

            return _container.GetRequiredService<T>();
        }

        /// <summary>
        /// Create a new container with the component registrations that have been made
        /// </summary>
        /// <returns></returns>
        protected void Build()
        {
            if (_container == null)
                _container = Builder.BuildServiceProvider();
        }
    }
}
