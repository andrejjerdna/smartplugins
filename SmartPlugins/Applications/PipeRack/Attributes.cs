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
        public int PolojenieVertikalno { get; set; }
        public int PolojeniePovorot { get; set; }
        public int PolojenieGorizontalno { get; set; }
        public int RNumberOfYarus { get; set; }
        public string DirectionOfYarus { get; set; }
        public string RNazvanie { get; set; }
        public string RType { get; set; }
    }
}
