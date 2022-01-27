using SmartPlugins.Common.Abstractions.ModelObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Common.Core.ModelAlgorithms
{
    public static class PartExtensions
    {
        public static bool SetClass(this IPart part, string className)
        {
            part.Class = className;
            part.Modify();
            return true;
        }
    }
}
