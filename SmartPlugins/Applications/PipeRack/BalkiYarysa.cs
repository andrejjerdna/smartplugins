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
        public List<Attributes> AttributesTraversyvprovete { get; set; }
       
        List<Frame> _FraMES;

        public List<Beam> _balki = new List<Beam>();
        public List<Beam> _traversyvprovete = new List<Beam>();

        public BalkiYarysa(List<Frame> FraMES)
        {
            _FraMES = FraMES;

        }

        public void Insert()
        {
            for (int i = 0; i < _FraMES[0]._Travers.Count(); i++)
            {
                Frame frame = new Frame();

                var Att = _FraMES[0]._Travers[i];
                var startPoint = new Point(_FraMES[0]._basePoint.X, Att.StartPoint.Y, Att.StartPoint.Z);
                var endPoint = new Point(_FraMES[0]._basePoint.X, Att.EndPoint.Y, Att.EndPoint.Z);

                var Att2 = _FraMES[1]._Travers[i];
                var startPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.StartPoint.Y, Att2.StartPoint.Z);
                var endPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.EndPoint.Y, Att2.EndPoint.Z);

                var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X); 

                _balki.Add(frame.Beam_main(AttributesProdolnie[i], startPoint, startPoint2)); // левая балка
                _balki.Add(frame.Beam_main(AttributesProdolnie[i], endPoint, endPoint2));     // правая балка  
           
               var _shagi = Shagtravers(_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X);

                for (int _i = 0; _i < _shagi.Count(); _i++)
                {

                    var startPoint3 = new Point(startPoint.X + _shagi[_i],   startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                    var endPoint3 = new Point(  endPoint.X + _shagi[_i],    endPoint.Y,     endPoint.Z + _shagi[_i] * uklon);
                    
                    _traversyvprovete.Add(frame.Beam_main(AttributesTraversyvprovete[i], startPoint3, endPoint3));
                }
 }

            
        }
        private List<double> Shagtravers(double n)
        {
            List<double> shagtravers = new List<double>();
            double a = Math.Truncate(n / 3000);
            double b = (n - (a  * 3000)) / 2 + 1500;

            if (Math.IEEERemainder(n, 3000) == 0)
            {
                b = 3000;
                shagtravers.Add(b);
            } 
            else
            {
                shagtravers.Add(b);
            }
            while (b < n - 3000)
            {
                b += 3000;
                shagtravers.Add(b);
            }
            return  shagtravers;
        }
    }
}
