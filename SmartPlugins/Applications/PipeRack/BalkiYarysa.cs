using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PipeRack
{
    class BalkiYarysa
    {
        public List<Attributes> AttributesProdolnie { get; set; }
        List<Frame> _FraMES;

        public BalkiYarysa(List<Frame> FraMES)
        {
            _FraMES = FraMES;
        }

        public void Insert()
        {
            for (int _count = 0; _count < _FraMES.Count() - 1; _count++)
            {
                for (int i = 0; i < _FraMES[0]._Travers.Count(); i++)
                { 
                    var Att = _FraMES[_count].Attributes[_count];
                var startPoint = new Point(_FraMES[_count]._basePoint.X, _FraMES[_count]._Travers[i].StartPoint.Y, _FraMES[_count]._Travers[i].StartPoint.Z);
                var endPoint = new Point(_FraMES[_count]._basePoint.X, _FraMES[_count]._Travers[i].EndPoint.Y, _FraMES[_count]._Travers[i].EndPoint.Z);

                var startPoint2 = new Point(_FraMES[_count + 1]._basePoint.X, _FraMES[_count + 1]._Travers[i].StartPoint.Y, _FraMES[_count + 1]._Travers[i].StartPoint.Z);
                var endPoint2 = new Point(_FraMES[_count + 1]._basePoint.X, _FraMES[_count + 1]._Travers[i].EndPoint.Y, _FraMES[_count + 1]._Travers[i].EndPoint.Z);

                Frame frame = new Frame();
                //  frame.Beam_main(Att, startPoint, endPoint);
                frame.Beam_main(AttributesProdolnie[_count], startPoint, startPoint2);
                frame.Beam_main(AttributesProdolnie[_count], endPoint, endPoint2);
                }
            }


        }


    }
}
