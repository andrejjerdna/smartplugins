using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public class SuperProdolnayaBalka : SuperBeam
    {
        public SuperProdolnayaBalka(Attributes att, Point startPoint, Point endPoint) : base(att, startPoint, endPoint)
        {
            _AttBeam = att;
            _AttBeam.RType = "Продольные балки";
        }
    }
}
