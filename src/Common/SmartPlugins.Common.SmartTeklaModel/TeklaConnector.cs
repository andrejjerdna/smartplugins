using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.SmartTeklaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tekla.Structures.Model;

namespace SmartTeklaModel
{
    //public class TeklaConnector : ITeklaConnector
    //{
    //    private Timer _timer;
    //    private bool _connectStatus;

    //    public Func<SmartModel> ConnectChanged { get; }

    //    public TeklaConnector()
    //    {
    //        _connectStatus = false;
    //        CreateTimer();
    //    }

    //    private void CreateTimer()
    //    {
    //        _timer = new Timer(1000);
    //        _timer.Elapsed += ConnectCheck;
    //        _timer.AutoReset = true;
    //        _timer.Enabled = true;
    //    }

    //    private void ConnectCheck(object source, ElapsedEventArgs e)
    //    {
    //        if(!_connectStatus)
    //        {
    //            var teklaModel = new Model();

    //            if(teklaModel.GetConnectionStatus())
    //            {
    //                ConnectChanged.Invoke();
    //            }
    //        }
    //    }
    //}
}
