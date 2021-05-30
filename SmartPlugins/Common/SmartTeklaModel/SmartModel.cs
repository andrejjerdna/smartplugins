using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel
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
                AttributesPath = TeklaModel.GetInfo().ModelPath + "\\attributes\\";
                FilterPath = AttributesPath;
                SmartPluginsPath = AttributesPath + "SmartPlugins\\";
            }
        }
    }
}
