using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.SmartExtensions;
using SmartPlugins.Common.SmartTeklaModel;
using SmartPlugins.Common.SmartTeklaModel.Rebar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;
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

            var reberNum = new RebarNumberator("REBAR_SEQ_NO");
            reberNum.RefreshNumbers(allReinforcements);
        }
    }
}
