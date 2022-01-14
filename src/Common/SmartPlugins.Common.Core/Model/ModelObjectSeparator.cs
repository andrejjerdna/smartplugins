using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using System.Collections.Generic;

namespace SmartPlugins.Common.Core.Model
{
    internal class ModelObjectSeparator
    {
        private readonly IModelObjectCreator _modelObjectCreator;
        public IEnumerable<IBeam> PolyBeamSeparate(IPolyBeam polybeam)
        {
            var points = polybeam.GetPoints();


        }
    }
}
