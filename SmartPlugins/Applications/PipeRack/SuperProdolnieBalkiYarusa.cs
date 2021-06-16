using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace PipeRack
{
    public class SuperProdolnieBalkiYarusa 
    {
        public Point StartPoint1 { get; set; }
        public Point StartPoint2 { get; set; }
        public Point EndPoint1 { get; set; }
        public Point EndPoint2 { get; set; }
        public bool Stroim { get; set; }
        public string Direction { get; set; }
        public Attributes Att { get; set; }
        public SuperProdolnayaBalka beamRight { get; set; }
        public SuperProdolnayaBalka beamLeft { get; set; }
        public SuperProdolnieBalkiYarusa() 
        {
            
        }

        public void Insert()
        {
            if (Direction == "Right")
            {
                if (Stroim)
                {
                    beamLeft = new SuperProdolnayaBalka(Att, StartPoint2, EndPoint2);
                    beamLeft.Insert();
                }
                beamRight = new SuperProdolnayaBalka(Att, StartPoint1, EndPoint1);
                beamRight.Insert();
            }
            if (Direction == "Left")
            {
                if (Stroim)
                {
                    beamRight = new SuperProdolnayaBalka(Att, StartPoint1, EndPoint1);
                    beamRight.Insert();
                }
                beamLeft = new SuperProdolnayaBalka(Att, StartPoint2, EndPoint2);
                beamLeft.Insert();
            }
        }
        public void Modify()
        {
            beamRight.StartPoint.X = StartPoint1.X;
            beamRight.EndPoint.X = EndPoint1.X;

            beamLeft.StartPoint.X = StartPoint2.X;
            beamLeft.EndPoint.X = EndPoint2.X;

            beamRight.Modify();
            beamLeft.Modify();
        }

        public void Delete()
        {
            beamRight.Delete();
            beamLeft.Delete();
        }

    }
}
