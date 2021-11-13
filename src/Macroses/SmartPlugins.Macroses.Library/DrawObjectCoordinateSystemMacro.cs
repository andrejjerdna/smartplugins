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
            var container = MacrosesContainerConfigure.GetContainer().Build();
            var model = container.Resolve<ISmartModel>();

            if (!model.ConnectionStatus) 
                return;

            var picker = new SmartPicker();

            var modelObject = picker.PickObject<ModelObject>(Picker.PickObjectEnum.PICK_ONE_OBJECT);

            if (modelObject == null) 
                return;

            DrawInTeklaModel.DrawTeklaObjectCoordinateSystem(modelObject.GetCoordinateSystem());
        }
    }
}
