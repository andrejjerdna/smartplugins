using SmartPlugins.Common.Abstractions.ModelObjects;
using System.Linq;

namespace SmartPlugins.Common.Core.Model
{
    internal class CastUnitReinforcementNumbering
    {
        ICastUnit _castUnit;
        public CastUnitReinforcementNumbering(ICastUnit castUnit)
        {
            _castUnit = castUnit;
        }

        public void PerformNumbering()
        {
            var reinforcement = _castUnit.GetReinforcement();

            if(reinforcement == null) 
                return;

            if (!reinforcement.Any())
                return;

            var reinforcementSort = reinforcement.OrderBy(r => r.RebarPos);

            var count = 0;

            foreach(var re in reinforcementSort)
                re.SetProperty("REBAR_SEQ_NO", count++);
        }
    }
}
