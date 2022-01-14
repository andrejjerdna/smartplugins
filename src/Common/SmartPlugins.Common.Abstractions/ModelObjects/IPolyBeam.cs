using System.Collections.Generic;

namespace SmartPlugins.Common.Abstractions.ModelObjects
{
    public interface IPolyBeam : IPart
    {
        IEnumerable<IPoint> GetPoints();
    }
}
