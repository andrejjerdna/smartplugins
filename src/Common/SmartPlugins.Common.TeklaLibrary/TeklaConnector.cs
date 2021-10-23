using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.Core;
using SmartPlugins.Common.TeklaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary
{
    public class TeklaConnector
    {
        private Timer _timer;
        private bool _connectStatus;

        public TeklaConnector()
        {
            _connectStatus = false;
            CreateTimer();
        }

        /// <summary>
        /// Connection status changed event
        /// </summary>
        public event EventHandler<ConnectArgs> ConnectionStatusChanged;

        private void CreateTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += ConnectCheck;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void ConnectCheck(object source, ElapsedEventArgs e)
        {
            if (!_connectStatus)
            {
                var teklaModel = new Model();

                if (teklaModel.GetConnectionStatus())
                {
                    var eventArgs = new ConnectArgs()
                    {
                        Message = "fff"
                    };

                    //OnConnectChanged();
                }
            }
        }

        private void OnConnectChanged(ConnectArgs e)
        {
            ConnectionStatusChanged?.Invoke(this, e);
        }
    }
}
