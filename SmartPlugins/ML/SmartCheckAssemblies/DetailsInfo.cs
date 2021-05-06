using SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SmartCheckAssemblies
{
    public class DetailsInfo
    {
        private Assembly _assembly;

        public DetailsInfo(Assembly assembly)
        {
            _assembly = assembly;
            GetDetails();
        }

        public void GetDetails()
        {
            var mainPart = _assembly.GetMainPart() as Part;

            if (mainPart == null)
                return;

            var cogX = mainPart.SmartGetPropertyDouble("COG_X");
            var cogY = mainPart.SmartGetPropertyDouble("COG_Y");
            var cogZ = mainPart.SmartGetPropertyDouble("COG_Z");

            var solidMainPart = mainPart.GetSolid();

            var secondariesParts = _assembly.GetSecondaries().OfType<Part>().ToList();
        }
    }
}
