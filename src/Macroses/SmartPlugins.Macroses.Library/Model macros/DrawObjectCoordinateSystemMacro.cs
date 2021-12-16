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
        private readonly ISmartModel _smartModel;
        private readonly ISmartPicker _smartPicker;
        private readonly IDrawInTeklaModel _drawInTeklaModel;

        public DrawObjectCoordinateSystemMacro(ISmartModel smartModel, 
                                               ISmartPicker smartPicker, 
                                               IDrawInTeklaModel drawInTeklaModel)
        {
            _smartModel = smartModel;
            _smartPicker = smartPicker;
            _drawInTeklaModel = drawInTeklaModel;
        }

        /// <inheritdoc/>
        public void RunLoop() => ErrorCatcher.Try(() => { while (true) { Macro(); } });

        /// <inheritdoc/>
        public void RunOnce() => ErrorCatcher.Try(() => { Macro(); });

        /// <summary>
        /// Logic of a macro
        /// </summary>
        private void Macro()
        {
            if (!_smartModel.ConnectionStatus)
                return;

            var modelObject = _smartPicker.PickObject<ModelObject>((int)Picker.PickObjectEnum.PICK_ONE_OBJECT);

            if (modelObject == null)
                return;

            _drawInTeklaModel.DrawTeklaObjectCoordinateSystem(modelObject.GetCoordinateSystem());
        }
    }
}
