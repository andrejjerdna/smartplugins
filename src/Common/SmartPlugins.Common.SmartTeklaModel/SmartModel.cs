using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.SmartTeklaModel
{
    public class SmartModel
    {
        public Model TeklaModel { get; }
        public bool ConnectionStatus { get; }
        public string FilterPath { get; }
        public string AttributesPath { get; }
        public string SmartPluginsPath { get; }

        public SmartModel()
        {
            TeklaModel = new Model();
            ConnectionStatus = TeklaModel.GetConnectionStatus();

            if (ConnectionStatus)
            {
                AttributesPath = Path.Combine(TeklaModel.GetInfo().ModelPath, "attributes");
                FilterPath = Path.Combine(AttributesPath);
                SmartPluginsPath = Path.Combine(AttributesPath,"SmartPlugins");
            }
        }
    }
}
