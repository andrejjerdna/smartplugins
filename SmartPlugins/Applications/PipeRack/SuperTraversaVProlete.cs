using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;


namespace PipeRack
{
    public class SuperTraversaVProlete : SuperBeam
    {
        public SuperTraversaVProlete(Attributes att, Point startPoint, Point endPoint) : base(att, startPoint, endPoint)
        {
            _AttBeam.RType = "Траверсы в пролете";
        }
    }
}
