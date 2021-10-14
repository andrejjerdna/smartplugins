using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.SmartWPFElements.SmartHelper
{
    public interface ISmartHelperApp
    {
        string Name { get; }
        Task<bool> Run();
    }
}
