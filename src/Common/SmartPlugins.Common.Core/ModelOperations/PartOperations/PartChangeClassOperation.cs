using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Core.ModelAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core.ModelOperations.PartOperations
{
    public class PartChangeClassOperation : IOperation
    {
        private readonly IPart _part;
        private readonly string _propertyName;
        public PartChangeClassOperation(IPart part, string propertyName)
        {
            _part = part;
            _propertyName = propertyName;
        }
        public void Run() => _part.SetClass(_propertyName);
    }
}
