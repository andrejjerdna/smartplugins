using SmartPlugins.Common.SmartTeklaModel.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Plugins;

namespace ConcreteFoundationPlugin
{
    public class PluginData : PluginDataBase
    {
        [StructuresField(nameof(Delta))] public double Delta;


    }
}
