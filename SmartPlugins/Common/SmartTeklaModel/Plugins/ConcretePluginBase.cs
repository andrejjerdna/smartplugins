using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;

namespace SmartPlugins.Common.SmartTeklaModel.Plugins
{
    public abstract class ConcretePluginBase : PluginBase
    {
        public Guid GUID;

        public int AddToCastUnit;
        public string Material;
        public string Class;
        public string AssemblyPrefix;
        public string PartPrefix;
        public int PartStartNumber;
        public int AssemblyStartNumber;
        public string Finish;
        public string AssemblyName;
        public string PartName;
        public int PourPhase;

        public string Attribute1;
        public string Attribute1_Value;
        public string Attribute2;
        public string Attribute2_Value;
        public string Attribute3;
        public string Attribute3_Value;

        public double L1;
        public double L2;
        public double L3;
        public double L4;
        public double L5;
        public double L6;

        public string Dist1;
        public string Dist2;
        public string Dist3;
        public string Dist4;
        public string Dist5;
        public string Dist6;

        public IEnumerable<double> DistanceList1;
        public IEnumerable<double> DistanceList2;
        public IEnumerable<double> DistanceList3;
        public IEnumerable<double> DistanceList4;
        public IEnumerable<double> DistanceList5;
        public IEnumerable<double> DistanceList6;

        public override List<InputDefinition> DefineInput()
        {
            return new List<InputDefinition>();
        }

        public void GetBaseValuesFromDialog(PluginDataBase data)
        {
            AddToCastUnit = data.AddToCastUnit; if (IsDefaultValue(AddToCastUnit)) AddToCastUnit = 1;

            Material = data.Material; if (IsDefaultValue(Material)) Material = GlobalParameters.BaseConcrete;
            Class = data.Class; if (IsDefaultValue(Class)) Class = GlobalParameters.BaseClass;
            PartName = data.PartName; if (IsDefaultValue(PartName)) PartName = "";
            Finish = data.Finish; if (IsDefaultValue(Finish)) Finish = "";
            PourPhase = data.PourPhase; if (IsDefaultValue(PourPhase)) PourPhase = 0;

            PartPrefix = data.PartPrefix; if (IsDefaultValue(PartPrefix)) PartPrefix = "";
            PartStartNumber = data.PartStartNumber; if (IsDefaultValue(PartStartNumber)) PartStartNumber = 1;

            AssemblyPrefix = data.AssemblyPrefix; if (IsDefaultValue(AssemblyPrefix)) AssemblyPrefix = "";
            AssemblyStartNumber = data.AssemblyStartNumber; if (IsDefaultValue(AssemblyStartNumber)) AssemblyStartNumber = 1;

            Attribute1 = data.Attribute1; if (IsDefaultValue(Attribute1)) Attribute1 = "33_W_beton";
            Attribute1_Value = data.Attribute1_Value; if (IsDefaultValue(Attribute1_Value)) Attribute1_Value = "W6";

            Attribute2 = data.Attribute2; if (IsDefaultValue(Attribute2)) Attribute2 = "33_F_beton";
            Attribute2_Value = data.Attribute2_Value; if (IsDefaultValue(Attribute2_Value)) Attribute2_Value = "F150";

            Attribute3 = data.Attribute3; if (IsDefaultValue(Attribute3)) Attribute3 = "";
            Attribute3_Value = data.Attribute3_Value; if (IsDefaultValue(Attribute3_Value)) Attribute3_Value = "";
        }
    }
}

