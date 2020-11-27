using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace SmartObjects
{
    interface IConcreteBase
    {
        void Insert();
    }

    interface IConcreteAssembly
    {
        Part GetMainPart();
        List<Part> GetSecondaryParts();
    }
}
