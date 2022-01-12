using SmartPlugins.Common.Abstractions.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public class SmartCastUnit : SmartAssembly, ICastUnit
    {
        private readonly Assembly _assembly;

        public SmartCastUnit(Assembly assembly) : base(assembly)
        {
            ModelObject = _assembly = assembly;
        }
        public IEnumerable<IReinforcement> GetReinforcement()
        {
            throw new NotImplementedException();
        }
    }
}
