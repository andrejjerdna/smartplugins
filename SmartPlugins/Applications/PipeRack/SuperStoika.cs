using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public class SuperStoika : SuperBeam
    {

        public SuperStoika(Attributes attBeam, Point startPoint, Point endPoint) : base(attBeam, startPoint, endPoint)
        {
                AttBeam.RType = "Стойки";
        }

    }
}
