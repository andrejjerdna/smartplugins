using Autofac;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace SmartPlugins.Macroses.Library
{
    /// <summary>
    /// Draw object coordinate system macro
    /// </summary>
    public class DrawObjectCoordinateSystemMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void RunLoop() => ErrorCatcher.Try(() => { while (true) { Macro(); } });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            var container = MacrosesContainerConfigure.GetContainer();
            var model = container.GetRequiredService<ISmartModel>();

            if (!model.ConnectionStatus)
                return;

            var picker = container.GetRequiredService<ISmartPicker>();
            var modelObject = picker.PickObject<ModelObject>((int)Picker.PickObjectEnum.PICK_ONE_OBJECT);

            if (modelObject == null)
                return;

            DrawInTeklaModel.DrawTeklaObjectCoordinateSystem(modelObject.GetCoordinateSystem());
        }
    }
}
