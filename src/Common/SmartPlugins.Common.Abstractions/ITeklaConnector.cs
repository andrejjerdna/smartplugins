using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Abstractions
{
    public interface ITeklaConnector
    {
        bool ConnectStatus { get; }
    }
}
