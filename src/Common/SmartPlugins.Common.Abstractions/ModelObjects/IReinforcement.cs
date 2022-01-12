using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface IReinforcement : IModelObject
    {
        string CastUnitPos { get; set; }

        int Number { get; set; }

        string RebarPos { get; set; }
    }
}
