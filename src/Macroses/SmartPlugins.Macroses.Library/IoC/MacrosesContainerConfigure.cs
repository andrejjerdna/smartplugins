﻿using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Exceptions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.Exceptions;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Assemblies;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Container configure for macros
    /// </summary>
    public class MacrosesContainerConfigure : ContainerConfigureBase
    {
        private static MacrosesContainerConfigure _container;

        /// <summary>
        /// .ctor
        /// </summary>
        private MacrosesContainerConfigure()
        {
            RegisterTypes();
        }

        /// <summary>
        /// Get container configure
        /// </summary>
        /// <returns></returns>
        public static MacrosesContainerConfigure GetContainer()
        {
            if (_container == null)
                _container = new MacrosesContainerConfigure();

            return _container;
        }

        /// <summary>
        /// Register types
        /// </summary>
        private void RegisterTypes()
        {
            RegisterType<ProgressState, IProgressState>();
            RegisterType<RebarNumerator, IRebarNumerator>();
            RegisterType<SmartPicker, ISmartPicker>();
            RegisterType<MainPartByWeight, IMainPartByWeight>();
            RegisterSingleInstanceType<SmartModel, ISmartModel>();
            RegisterSingleInstanceType<MacrosesProgressLogger, IProgressLogger>();
            RegisterSingleInstanceType<ExceptionsLogger, IExceptionsLogger>();
        }
    }
}
