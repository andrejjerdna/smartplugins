using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface IAssembly : IModelObject
    {
        IPart GetMainPart();

        IEnumerable<IPart> GetSecondaries();

        void SetMainPart(IPart part);
    }
}
