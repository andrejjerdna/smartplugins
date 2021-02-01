using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel.PluginAnalysis
{
    public class UserRun
    {
        private Model _model;
        public string ID { get; }
        public System.Net.IPAddress IP { get; }
        public string TeklaVersion { get; }

        public UserRun(Model model, string id)
        {
            _model = model;
            ID = id;

            IP = GetUserIP();
        }

       /* private void GetTeklaVersion()
        {
            _model.GetInfo().
        }*/

        private System.Net.IPAddress GetUserIP()
        {
            var host = System.Net.Dns.GetHostName();

           return System.Net.Dns.GetHostEntry(host).AddressList[0];            
        }
    }
}
