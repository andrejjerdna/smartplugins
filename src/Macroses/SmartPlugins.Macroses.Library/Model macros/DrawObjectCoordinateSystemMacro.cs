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
        public void Run()
        {
            ErrorCatcher.Try(() =>
            {
                var container = MacrosesContainerConfigure.GetContainer().Build();
                var model = container.Resolve<ISmartModel>();

                if (!model.ConnectionStatus)
                    return;

                var picker = container.Resolve<ISmartPicker>();
                var modelObject = picker.PickObject<ModelObject, Picker.PickObjectEnum>(Picker.PickObjectEnum.PICK_ONE_OBJECT.ToString());

                if (modelObject == null)
                    return;

                DrawInTeklaModel.DrawTeklaObjectCoordinateSystem(modelObject.GetCoordinateSystem());
            });
        }
    }
}
