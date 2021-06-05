using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    public class SuperColumn : SuperBeam
    {
        
        public SuperColumn(Attributes attBeam, Point startPoint, Point endPoint) : base (attBeam, startPoint, endPoint)
        {
           
            if(AttBeam != null)
            {
                AttBeam.DirectionOfYarus = "Center";
                AttBeam.RType = "Колонны";
            }

        }
    }
}
