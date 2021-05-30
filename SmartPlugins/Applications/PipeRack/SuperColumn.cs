using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    class SuperColumn : SuperBeam
    {
        public SuperColumn(Attributes att, Point startPoint, Point endPoint): base (att, startPoint, endPoint)
        {
            _AttBeam.DirectionOfYarus = "Center";
            _AttBeam.RType = "Колонны";
        }
    }
}
