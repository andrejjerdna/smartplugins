using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using SmartGeometry;

namespace PipeRack
{
    public class Attributes
    {
        public string Name { get; set; }
        public string Profile { get; set; }
        public string Material { get; set; }
        public string Class { get; set; }
        public string PrefixSborki { get; set; }
        public string NomerSborki { get; set; }
        public string PolojenieVertikalno { get; set; }
        public string PolojeniePovorot{ get; set; }
        public string PolojenieGorizontalno { get; set; }
    }
}
