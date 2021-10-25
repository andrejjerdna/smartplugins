using Autofac;

namespace SmartPlugins.Common.Core
{
    public class ContainerConfigure
    {
        private readonly ContainerBuilder _builder;

        public static IContainer Container { get; private set; }

        public ContainerConfigure()
        {
            _builder = new ContainerBuilder();
        }

        public void RegisterType<Class, Interface>() => _builder.RegisterType<Class>().As<Interface>();

        public void RegisterSingleInstanceType<Class, Interface>() => _builder.RegisterType<Class>().As<Interface>().SingleInstance();

        public IContainer BuildContainer() => _builder.Build();
    }
}
