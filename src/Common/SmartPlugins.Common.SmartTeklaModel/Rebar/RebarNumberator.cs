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
        private Assembly _assembly;
        private string _attributeForNumber;
        private readonly string _separator = "-";

        public string Prefix { get; set; }
        
        public RebarNumberator(Assembly assembly, string attributeForNumber)
        {
            _assembly = assembly;
            _attributeForNumber = attributeForNumber;

            Prefix = string.Empty;
        }

        public void RefreshNumbers()
        {
            //var allRebars = _assembly.GetAllRebars()
            //    .GroupBy(r => new { Lenght = r.SmartGetPropertyDouble("LENGTH"), Weight = r.SmartGetPropertyDouble("WEIGHT"), r.Size, r.Grade })
            //    .OrderByDescending(g => g.Key.Grade)
            //    .OrderByDescending(g => g.Key.Size)
            //    .OrderByDescending(g => g.Key.Lenght)
            //    .OrderByDescending(g => g.Key.Weight);

            var allRebars = _assembly.GetAllReinforcements()
                .GroupBy(r => new { Pos = r.SmartGetPropertyString("REBAR_POS"), Lenght = r.SmartGetPropertyDouble("LENGTH") })
                .OrderByDescending(g => g.Key.Lenght).ToList();

            var count = 1;

            foreach (var rebarGroup in allRebars)
            {
                foreach (var rebar in rebarGroup)
                {
                    WriteUDA(rebar, count);
                }

                count++;
            }
        }

        private void WriteUDA(ModelObject rebar, int count)
        {
            rebar.SetUserProperty(_attributeForNumber, count);
            rebar.Modify();
        }
    }
}
