using SmartPlugins.Common.Abstractions.ModelObjects;
using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartPlugins.Common.TeklaLibrary.Entities
{
    public class SmartPolyBeam : SmartPart, IPolyBeam
    {
        private readonly PolyBeam _polyBeam;
        public SmartPolyBeam(PolyBeam polyBeam) : base(polyBeam)
        {
            _polyBeam = polyBeam;
        }

        public IEnumerable<IPoint> GetPoints()
        {
            foreach(var contourPoint in _polyBeam.Contour.ContourPoints)
                if(contourPoint is Point point)
                    yield return new SmartPoint(point);
        }
    }
}
