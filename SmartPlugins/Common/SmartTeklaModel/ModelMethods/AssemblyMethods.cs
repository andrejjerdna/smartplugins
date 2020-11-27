using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartTeklaModel
{
    class AssemblyMethods
    {
        public static void AddPartToCastUnit(Assembly assembly, Part part)
        {
            assembly.Add(part);
            assembly.Modify();
        }
    }
}
