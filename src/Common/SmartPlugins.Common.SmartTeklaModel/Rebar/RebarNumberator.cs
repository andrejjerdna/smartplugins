using SmartPlugins.Common.SmartExtensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;


namespace SmartPlugins.Common.SmartTeklaModel.Rebar
{
    public class RebarNumberator
    {
        private string _attributeForNumber;
        
        public RebarNumberator(string attributeForNumber)
        {
            _attributeForNumber = attributeForNumber;
        }

        private IEnumerable<ReinforcementNumberingItem> GetReinforcementNumberingItems(IEnumerable<Reinforcement> reinforcements)
        {
            foreach (var rebar in reinforcements)
            {
                var item  = new ReinforcementNumberingItem()
                {
                    OriginObject = rebar,
                    CastUnitPos = rebar.SmartGetPropertyString("CAST_UNIT_POS"),
                    RebarPos = rebar.SmartGetPropertyString("REBAR_POS"),
                };

                yield return item;
            }
        }

        public void RefreshNumbers(IEnumerable<Reinforcement> reinforcements)
        {
            var sort = GetReinforcementNumberingItems(reinforcements.ToList()).ToList();

            var rebarsGroups = sort.GroupBy(r => r.CastUnitPos)
                                                .Select(group => group)
                                                .Select(group=>group.GroupBy(r => r.RebarPos).OrderBy(g => g.Key).Select(g => g.ToList()));

            foreach(var rebarGoup in rebarsGroups)
            {
                var count = 1;
                foreach(var gr in rebarGoup)
                {
                    foreach (var rebar in gr)
                        rebar.Number = count;

                    count++;
                }
            }

            var allRebar = new ConcurrentBag<ReinforcementNumberingItem>(rebarsGroups.SelectMany(gr => gr).SelectMany(gr => gr).Select(r => r));

            WriteUDA(allRebar);
        }

        public void WriteUDA(ConcurrentBag<ReinforcementNumberingItem> reinforcements)
        {
            Parallel.ForEach(reinforcements, (rebar) =>
            {
                rebar.OriginObject.SetUserProperty(_attributeForNumber, rebar.Number);
            });
        }
    }
}
