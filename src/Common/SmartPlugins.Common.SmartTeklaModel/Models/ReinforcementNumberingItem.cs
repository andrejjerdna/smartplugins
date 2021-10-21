using System;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    public class ReinforcementNumberingItem
    {
        public string CastUnitPos{ get; set; }

        public int Number { get; set; }

        public string RebarPos { get; set; }

        public Reinforcement OriginObject { get; set; }
    }
}
