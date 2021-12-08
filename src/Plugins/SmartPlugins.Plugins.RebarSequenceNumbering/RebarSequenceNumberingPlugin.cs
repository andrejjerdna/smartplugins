using Autofac;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using SmartPlugins.Macroses.Library;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Plugins;

namespace SmartPlugins.Plugins.RebarSequenceNumbering
{
    [Plugin("sp_RebarSequenceNumbering")]
    [PluginUserInterface("SmartPlugins.Plugins.RebarSequenceNumbering.Form1")]
    [InputObjectDependency(InputObjectDependency.NOT_DEPENDENT)]
    public class RebarSequenceNumberingPlugin : PluginBase
    {
        private readonly IContainer _container;

        public RebarSequenceNumberingPlugin()
        {
            _container = MacrosContainerConfigure.GetContainer().Build();
        }

        public override List<InputDefinition> DefineInput()
        {
            return new List<InputDefinition>();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            var rebarNumerator = _container.Resolve<IRebarNumerator>();
            rebarNumerator.RefreshAllNumbers("REBAR_SEQ_NO");

            return true;
        }
    }
}
