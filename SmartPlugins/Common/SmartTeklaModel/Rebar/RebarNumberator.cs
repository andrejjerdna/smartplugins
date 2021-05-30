using SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel.Rebar
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
            var allRebars = _assembly.GetAllRebars()
                .OrderByDescending(r => r.Size)
                .OrderByDescending(r => r.Class);
        }

        private void WriteUDA(IEnumerable<BaseRebarGroup> rebarGroups)
        {
            var count = 0;

            var localPrefix = Prefix + _separator;

            if (string.IsNullOrEmpty(Prefix))
                localPrefix = string.Empty;

            foreach (var rebar in rebarGroups)
            {
                count++;

                var number = string.Format("{0}+{1}", localPrefix, count);
                rebar.SetUserProperty(_attributeForNumber, number);
            }
        }
    }
}
