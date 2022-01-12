using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface ICastUnit : IAssembly
    {
        IEnumerable<IReinforcement> GetReinforcement();
    }
}
