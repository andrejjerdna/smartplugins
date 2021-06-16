using SmartExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace PipeRack
{
    class SuperStoikiVProlete
    {
        public Attributes Att { get; set; }
        public List<SuperStoika> Stoiki = new List<SuperStoika>();

        private SuperTraversyVProleteYarysa _stvpy; 

        public SuperStoikiVProlete(SuperTraversyVProleteYarysa stvpy)
        {
            _stvpy = stvpy;
        }

        public void Insert()
        {
            foreach (SuperTraversaVProlete stvp in _stvpy.Traversy)
            {

               var hTraversy = stvp._beam.SmartGetPropertyDouble("WIDTH");
                Point startPointST = new Point(stvp.StartPoint.X, stvp.StartPoint.Y, stvp.StartPoint.Z - hTraversy);
                Point EndPointST = new Point(stvp.StartPoint.X, stvp.StartPoint.Y, stvp.StartPoint.Z - hTraversy - 500);

                 SuperStoika stoika = new SuperStoika(Att, startPointST, EndPointST);
                stoika.Insert();
                Stoiki.Add(stoika);
            }
        }
    }
}
