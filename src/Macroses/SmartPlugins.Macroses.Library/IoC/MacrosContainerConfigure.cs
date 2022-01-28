using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.Abstractions.Pickers;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.Core.ModelOperations.AssemblyOperations;
using SmartPlugins.Common.TeklaLibrary;
using SmartPlugins.Common.TeklaLibrary.Assemblies;
using SmartPlugins.Common.TeklaLibrary.Parts;
using SmartPlugins.Common.TeklaLibrary.Pickers;
using SmartPlugins.Common.TeklaLibrary.Points;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Container configure for macros
    /// </summary>
    public sealed class MacrosContainerConfigure : ContainerConfigureBase
    {
        private static MacrosContainerConfigure _container;

        /// <summary>
        /// .ctor
        /// </summary>
        private MacrosContainerConfigure()
        {
            RegisterTypes();
        }

        /// <summary>
        /// Get new instance the container and build of the container
        /// </summary>
        /// <returns></returns>
        public static MacrosContainerConfigure GetContainer()
        {
            if (_container == null)
            {
                _container = new MacrosContainerConfigure();
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
            RegisterType<OperationsRunner, IOperationsRunner>();

            RegisterSingleInstanceType<SmartModel, ISmartModel>();
            RegisterSingleInstanceType<MacrosProgressLogger, IProgressLogger>();

            #region Macros

            RegisterType<RebarSequenceNumberingMacro>();
            RegisterType<MainPartByWeightMacro>();
            RegisterType<DrawObjectCoordinateSystemMacro>();
            RegisterType<PointsReverseMacro>();
            RegisterType<RoundingCoordinatesPointsMacro>();
            RegisterType<DrawInModelCoordinatesPointsMacro>();
            RegisterType<NumberingSecondariesPartsMacro>();

            #endregion

            #region Operations

            RegisterType<MainPartByMaxWeightOperation>();

            #endregion
        }
    }
}
