using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    class CreateColumn : CreateBeam
    {
        public CreateColumn(Attributes att, Point startPoint, Point endPoint)
        {
            _AttBeam = att;
            _StartPoint = startPoint;
            _EndtPoint = endPoint;
            _AttBeam.DirectionOfYarus = "Center";
        }
    }
}
