using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.TeklaLibrary;
using Reinforcement = Tekla.Structures.Model.Reinforcement;

namespace SmartPlugins.Macroses.Library
{
    public class RebarSequenceNumberingMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void Run()
        {
            var model = new SmartModel();
            var allReinforcements = model.GetAllObjects<Reinforcement>(true);
            var reberNum = new RebarNumerator("REBAR_SEQ_NO");
            reberNum.RefreshNumbers(allReinforcements);
        }
    }
}
