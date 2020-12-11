using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;

namespace SmartTeklaModel.Plugins
{
    public class PluginDataBase
    {
        //Свойства создаваемой детали.
        [StructuresField(nameof(Material))] public string Material;
        [StructuresField(nameof(Class))] public string Class;
        [StructuresField(nameof(AssemblyPrefix))] public string AssemblyPrefix;
        [StructuresField(nameof(PartPrefix))] public string PartPrefix;
        [StructuresField(nameof(AssemblyStartNumber))] public int AssemblyStartNumber;
        [StructuresField(nameof(PartStartNumber))] public int PartStartNumber;
        [StructuresField(nameof(Finish))] public string Finish;
        [StructuresField(nameof(AssemblyName))] public string AssemblyName;
        [StructuresField(nameof(PartName))] public string PartName;
        [StructuresField(nameof(PourPhase))] public int PourPhase;

        [StructuresField(nameof(AddToCastUnit))] public int AddToCastUnit;

        //Пользовательские аттрибуты.
        [StructuresField(nameof(Attribute1))] public string Attribute1;
        [StructuresField(nameof(Attribute1_Value))] public string Attribute1_Value;
        [StructuresField(nameof(Attribute2))] public string Attribute2;
        [StructuresField(nameof(Attribute2_Value))] public string Attribute2_Value;
        [StructuresField(nameof(Attribute3))] public string Attribute3;
        [StructuresField(nameof(Attribute3_Value))] public string Attribute3_Value;

        //Числовые параметры.
        [StructuresField(nameof(L1))] public double L1;
        [StructuresField(nameof(L2))] public double L2;
        [StructuresField(nameof(L3))] public double L3;
        [StructuresField(nameof(L4))] public double L4;
        [StructuresField(nameof(L5))] public double L5;
        [StructuresField(nameof(L6))] public double L6;

        //Шаги.
        [StructuresField(nameof(Dist1))] public string Dist1;
        [StructuresField(nameof(Dist2))] public string Dist2;
        [StructuresField(nameof(Dist3))] public string Dist3;
        [StructuresField(nameof(Dist4))] public string Dist4;
        [StructuresField(nameof(Dist5))] public string Dist5;
        [StructuresField(nameof(Dist6))] public string Dist6;
    }
}
