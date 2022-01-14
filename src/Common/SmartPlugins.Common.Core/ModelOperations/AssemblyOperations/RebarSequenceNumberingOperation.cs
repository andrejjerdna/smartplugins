using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.ModelObjects;
using SmartPlugins.Common.Core.Model;

namespace SmartPlugins.Common.Core.ModelOperations.AssemblyOperations
{
    public class RebarSequenceNumberingOperation : IOperation
    {
        private readonly ICastUnit _castUnit;
        public RebarSequenceNumberingOperation(ICastUnit castUnit)
        {
            _castUnit = castUnit;
        }
        public void Run() => new CastUnitReinforcementNumbering(_castUnit).PerformNumbering();
    }
}
