using SmartPlugins.Common.SmartExtensions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Task = System.Threading.Tasks.Task;

namespace SmartPlugins.Common.SmartTeklaModel.Rebar
{
    public class RebarNumberator
    {
        private string _attributeForNumber;

        public RebarNumberator(string attributeForNumber)
        {
            _attributeForNumber = attributeForNumber;
        }

        public void RefreshNumbers(IEnumerable<Reinforcement> reinforcements)
        {
            var sort = GetReinforcementNumberingItems(reinforcements).ToList();

            var rebarsGroups = GetNumbersReinforcementItems(sort);

            WriteUDA(rebarsGroups);
        }

        public void WriteUDA(IEnumerable<ReinforcementNumberingItem> reinforcements)
        {
            var rebars = new ConcurrentBag<ReinforcementNumberingItem>(reinforcements);

            Parallel.ForEach(rebars, (rebar) =>
            {
                rebar.OriginObject.SetUserProperty(_attributeForNumber, rebar.Number);
            });
        }

        private IEnumerable<ReinforcementNumberingItem> GetReinforcementNumberingItems(IEnumerable<Reinforcement> reinforcements)
        {
            var result = new ConcurrentBag<ReinforcementNumberingItem>();

            var rebars = new ConcurrentBag<Reinforcement>(reinforcements);

            Parallel.ForEach(rebars, (rebar) =>
            {
                var sNames = new ArrayList();
                sNames.Add("CAST_UNIT_POS");
                sNames.Add("REBAR_POS");

                var sValues = new Hashtable(sNames.Count);

                rebar.GetAllReportProperties(sNames, new ArrayList(), new ArrayList(), ref sValues);

                result.Add(new ReinforcementNumberingItem()
                {
                    OriginObject = rebar,
                    CastUnitPos = (string)sValues["CAST_UNIT_POS"],
                    RebarPos = (string)sValues["REBAR_POS"]
                });
            });

            return result;
        }

        private IEnumerable<ReinforcementNumberingItem> GetNumbersReinforcementItems(IEnumerable<ReinforcementNumberingItem> sort)
        {
            
            var rebarsGroups = sort.GroupBy(r => r.CastUnitPos)
                       .Select(group => group)
                       .Select(group => group.GroupBy(r => r.RebarPos).OrderBy(g => g.Key).Select(g => g));

            foreach (var rebarGoup in rebarsGroups)
            {
                var count = 1;
                foreach (var gr in rebarGoup)
                {
                    foreach (var rebar in gr)
                        rebar.Number = count;

                    count++;
                }
            }

            return rebarsGroups.SelectMany(gr => gr).SelectMany(gr => gr).Select(r => r);
        }

    }
}
