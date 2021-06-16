using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public class SuperTraversyVProleteYarysa
    {
        public Attributes Att { get; set; }
        public List<SuperTraversaVProlete> Traversy = new List<SuperTraversaVProlete>();
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public List<double> Shagi { get; set; }
        public double Yklon { get; set; }

        public SuperTraversyVProleteYarysa(List<double> shagi, double yklon)
            {
                Shagi = shagi;
                Yklon = yklon;
            }
        public void Insert()
        {
            
            foreach (double xShag in Shagi)
            {
                Point startPointN = new Point(StartPoint.X + xShag, StartPoint.Y, StartPoint.Z + xShag * Yklon);
                Point endPointN = new Point(EndPoint.X + xShag, EndPoint.Y, EndPoint.Z + xShag * Yklon);

                SuperTraversaVProlete traversa = new SuperTraversaVProlete(Att, startPointN, endPointN);
                traversa.Insert();
                Traversy.Add(traversa);
            }
        }

        public void Modify()
        {
            var i = 0;

            if(Shagi.Count() < Traversy.Count())
            {
                for (int f = Shagi.Count(); f < Traversy.Count(); f++)
                {
                    Traversy[f].Delete();
                }
                Traversy.RemoveRange(Shagi.Count(), Traversy.Count()- Shagi.Count());
            }
            if (Shagi.Count() > Traversy.Count())
            {
                for (int f = Traversy.Count(); f < Shagi.Count(); f++)
                {
                    Point startPointN = new Point(StartPoint.X + Shagi[f], StartPoint.Y, StartPoint.Z + Shagi[f] * Yklon);
                    Point endPointN = new Point(EndPoint.X + Shagi[f], EndPoint.Y, EndPoint.Z + Shagi[f] * Yklon);

                    SuperTraversaVProlete traversa = new SuperTraversaVProlete(Att, startPointN, endPointN);
                    traversa.Insert();
                    Traversy.Add(traversa);
                }
                Traversy.RemoveRange(Shagi.Count(), Traversy.Count() - Shagi.Count());
            }

            foreach (double xShag in Shagi)
            {
                Traversy[i].StartPoint = new Point(StartPoint.X + xShag, StartPoint.Y, StartPoint.Z + xShag * Yklon);
                Traversy[i].EndPoint = new Point(EndPoint.X + xShag, EndPoint.Y, EndPoint.Z + xShag * Yklon);
                Traversy[i].Modify();
                i++;
            }
        }

        public void Delete()
        {
            foreach (SuperTraversaVProlete trav in Traversy)
            {
                trav.Delete();
            }

        }



    }
}
