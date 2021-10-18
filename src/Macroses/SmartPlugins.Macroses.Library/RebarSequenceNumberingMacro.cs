using SmartExtensions;
using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.SmartTeklaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tekla.Structures.Model;

namespace SmartPlugins.Macroses.Library
{
    public class RebarSequenceNumberingMacro : ITeklaMacro
    {
        /// <inheritdoc/>
        public void Run()
        {
            var model = new SmartModel();

            var selectedAssemblies = model.TeklaModel.GetSelectedObjects<Assembly>();

        }
    }
}
