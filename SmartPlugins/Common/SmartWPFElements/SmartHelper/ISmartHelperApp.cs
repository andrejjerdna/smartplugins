using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWPFElements.SmartHelper
{
    public interface ISmartHelperApp
    {
        Task<bool> Run();
    }
}
