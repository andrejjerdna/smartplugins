using SmartPlugins.Common.Abstractions;
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
            var model = new SmartModel();

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
