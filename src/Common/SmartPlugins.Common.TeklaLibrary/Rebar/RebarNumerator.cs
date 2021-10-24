using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Common.TeklaLibrary.CommonParameters;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary
{
    /// <summary>
    /// Rebar numerator
    /// </summary>
    public class RebarNumerator : IRebarNumerator
    {
        private string _attributeForNumber;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="attributeForNumber"></param>
        public RebarNumerator(string attributeForNumber)
        {
            _attributeForNumber = attributeForNumber;
        }

        /// <summary>
        /// Refresh numbers for list of reinforcements
        /// </summary>
        /// <param name="reinforcements"></param>
        public void RefreshNumbers(IEnumerable<Reinforcement> reinforcements)
        {
            var sort = GetReinforcementNumberingItems(reinforcements).ToList();

            var rebarsGroups = GetNumbers(sort);

            WriteUDA(rebarsGroups);
        }

        /// <summary>
        /// Write UDA for reinforcements
        /// </summary>
        /// <param name="reinforcements"></param>
        private void WriteUDA(IEnumerable<ReinforcementNumberingItem> reinforcements)
        {
            var rebars = new ConcurrentBag<ReinforcementNumberingItem>(reinforcements);

            Parallel.ForEach(rebars, (rebar) =>
            {
                rebar.OriginObject.SetUserProperty(_attributeForNumber, rebar.Number);
            });
        }

        /// <summary>
        /// Get list of reinforcement numbering items
        /// </summary>
        /// <param name="reinforcements"></param>
        /// <returns></returns>
        private IEnumerable<ReinforcementNumberingItem> GetReinforcementNumberingItems(IEnumerable<Reinforcement> reinforcements)
        {
            var result = new ConcurrentBag<ReinforcementNumberingItem>();

            var rebars = new ConcurrentBag<Reinforcement>(reinforcements);

            Parallel.ForEach(rebars, (rebar) =>
            {
                var reportProperties = new ReportProperties(rebar);

                reportProperties.AddStringAttribute(CommonConcreteParts.CastUnitPos);
                reportProperties.AddStringAttribute(CommonConcreteParts.RebarPos);

                reportProperties.RefreshReportProperties();

                result.Add(new ReinforcementNumberingItem()
                {
                    OriginObject = rebar,
                    CastUnitPos = reportProperties.GetAttribute<string>(CommonConcreteParts.CastUnitPos),
                    RebarPos = reportProperties.GetAttribute<string>(CommonConcreteParts.RebarPos)
                });
            });

            return result;
        }

        /// <summary>
        /// Get numbers for list of reinforcement numbering items
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        private IEnumerable<ReinforcementNumberingItem> GetNumbers(IEnumerable<ReinforcementNumberingItem> sort)
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
