using Autofac;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Abstractions.TeklaStructures;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SmartPlugins.Macroses.Library
{
    public class RebarSequenceNumberingMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void Run()
        {
            try
            {
                var container = MacrosesContainerConfigure.GetContainer().Build();
                var rebarNumerator = container.Resolve<IRebarNumerator>();
                rebarNumerator.RefreshAllNumbers("REBAR_SEQ_NO");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
