using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.Geometry;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace SmartPlugins.Macros.Library
{
    /// <summary>
    /// Reverse part points
    /// </summary>
    public class RoundingCoordinatesPointsMacro : ITeklaMacro
    {
        private readonly ISmartModel _smartModel;
        private readonly ISmartPicker _smartPicker;
        private readonly IPartOperations _partOperations;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="smartModel"></param>
        /// <param name="smartPicker"></param>
        /// <param name="partOperations"></param>
        public RoundingCoordinatesPointsMacro(ISmartModel smartModel,
                                              ISmartPicker smartPicker,
                                              IPartOperations partOperations)
        {
            _smartModel = smartModel;
            _smartPicker = smartPicker;
            _partOperations = partOperations;
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

            _partOperations.RoundingCoordinatesPointsModelObject(modelObject);
            _smartModel.CommitChanges();
        }
    }
}
