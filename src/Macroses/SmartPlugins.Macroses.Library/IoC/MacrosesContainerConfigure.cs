using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.Abstractions.Picker;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Assemblies;
using SmartPlugins.Common.TeklaLibrary.Parts;
using SmartPlugins.Common.TeklaLibrary.Picker;
using SmartPlugins.Common.TeklaLibrary.Points;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Container configure for macros
    /// </summary>
    public sealed class MacrosesContainerConfigure : ContainerConfigureBase
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
        /// Get new instance the container and build of the container
        /// </summary>
        /// <returns></returns>
        public static MacrosesContainerConfigure GetContainer()
        {
            if (_container == null)
            {
                _container = new MacrosesContainerConfigure();
                _container.Build();
            }

            return _container;
        }

        /// <summary>
        /// Register types
        /// </summary>
        private void RegisterTypes()
        {
            RegisterType<RebarNumerator, IRebarNumerator>();
            RegisterType<SmartPicker, ISmartPicker>();
            RegisterType<DrawInTeklaModel, IDrawInTeklaModel>();
            RegisterType<PartOperations, IPartOperations>();
            RegisterType<PointOperations, IPointOperations>();
            RegisterType<AssemblyOperations, IAssemblyOperations>();
            RegisterType<PickerObjects, IPickerObjects>();
            
            RegisterSingleInstanceType<SmartModel, ISmartModel>();
            RegisterSingleInstanceType<MacrosesProgressLogger, IProgressLogger>();
            RegisterSingleInstanceType<OperationsRunner, IOperationsRunner>();

            #region Macros

            RegisterType<RebarSequenceNumberingMacro>();
            RegisterType<MainPartByWeightMacro>();
            RegisterType<DrawObjectCoordinateSystemMacro>();
            RegisterType<PointsReverseMacro>();
            RegisterType<RoundingCoordinatesPointsMacro>();
            RegisterType<DrawInModelCoordinatesPointsMacro>();
            RegisterType<MainPartByWeightMacroAndNumberingSecondariesMacro>();

            #endregion
        }
    }
}
