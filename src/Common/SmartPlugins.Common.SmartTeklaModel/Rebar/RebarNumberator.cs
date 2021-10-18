using SmartPlugins.Common.SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void RefreshNumbers(Assembly assembly)
        {
            var allRebars = assembly.GetAllReinforcements()
                .GroupBy(r => r.SmartGetPropertyString("REBAR_POS"))
                .ToConcurrentBag();

            var count = 0;

            Parallel.ForEach(allRebars, rebarGroup =>
            {
                foreach (var rebar in rebarGroup)
                {
                    WriteUDA(rebar, count);
                }
            });
        }

        private void WriteUDA(ModelObject rebar, int count)
        {
            rebar.SetUserProperty(_attributeForNumber, count);
            rebar.Modify();
        }
    }
}
