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
        public List<Attributes> AttributesProdolnieRight { get; set; }
        public List<Attributes> AttributesProdolnieLeft { get; set; }
        public List<Attributes> AttributesTraversyvprovete { get; set; }

        List<Frame> _FraMES;
        private bool checkBeamR = true;
        private bool checkBeamL = true;

        public List<Beam> _balki = new List<Beam>();
        public List<Beam> _traversyvprovete = new List<Beam>();

        double EndST = 0;

        public BalkiYarysa(List<Frame> FraMES)
        {
            _FraMES = FraMES;
        }
        public BalkiYarysa()
        {
            
        }

        public void Insert()
        {
            
            for (int i = 0; i < _FraMES[0]._TraversRight.Count(); i++) // правые балки половина балок
            {
                if(AttributesProdolnieRight[i] !=null)
                {
                    var Att = _FraMES[0]._TraversRight[i];
                    var startPoint = new Point(_FraMES[0]._basePoint.X, Att.StartPoint.Y, Att.StartPoint.Z);
                    var endPoint = new Point(_FraMES[0]._basePoint.X, Att.EndPoint.Y, Att.EndPoint.Z);

                    var Att2 = _FraMES[1]._TraversRight[i];
                    var startPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.StartPoint.Y, Att2.StartPoint.Z);
                    var endPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.EndPoint.Y, Att2.EndPoint.Z);

                    var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X);
                    var _shagi = Shagtravers(_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X, 3000);
                    Frame frame = new Frame();

                    Check(i);

                    if (!checkBeamR)
                    {
                        _balki.Add(frame.Beam_main(AttributesProdolnieRight[i], startPoint, startPoint2)); // левая балка
                        checkBeamL = true;

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.Beam_main(AttributesTraversyvprovete[i], startPoint3, endPoint3)); // траверса в пролете
                            double H = 0;
                            _traversyvprovete[_i].GetReportProperty("WIDTH", ref H);
                            Point startPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - H);
                            Point EndPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - EndST);


                            frame.Beam_main(AttributesTraversyvprovete[i], startPointST, EndPointST);
                        }
                    }
                    else
                    {
                        _balki.Add(frame.Beam_main(AttributesProdolnieRight[i], startPoint, startPoint2)); // левая балка
                        _balki.Add(frame.Beam_main(AttributesProdolnieRight[i], endPoint, endPoint2));     // правая балка  

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.Beam_main(AttributesTraversyvprovete[i], startPoint3, endPoint3)); // траверса в пролете
                        }
                    }
                }
            }
                

            for (int i = 0; i < _FraMES[0]._TraversLeft.Count(); i++) // левые балки половина балок
            {
                if (AttributesProdolnieLeft[i] != null)
                {
                    var Att = _FraMES[0]._TraversLeft[i];
                    var startPoint = new Point(_FraMES[0]._basePoint.X, Att.StartPoint.Y, Att.StartPoint.Z);
                    var endPoint = new Point(_FraMES[0]._basePoint.X, Att.EndPoint.Y, Att.EndPoint.Z);

                    var Att2 = _FraMES[1]._TraversLeft[i];
                    var startPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.StartPoint.Y, Att2.StartPoint.Z);
                    var endPoint2 = new Point(_FraMES[1]._basePoint.X, Att2.EndPoint.Y, Att2.EndPoint.Z);

                    var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X);
                    var _shagi = Shagtravers(_FraMES[1]._basePoint.X - _FraMES[0]._basePoint.X, 3000);
                    Frame frame = new Frame();

                    Check2(i);

                    if (!checkBeamL)
                    {
                        _balki.Add(frame.Beam_main(AttributesProdolnieLeft[i], endPoint, endPoint2));     // правая балка
                        checkBeamR = true;
                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.Beam_main(AttributesTraversyvprovete[i], startPoint3, endPoint3)); // траверса в пролете
                            double H = 0;
                            _traversyvprovete[_i].GetReportProperty("WIDTH", ref H);
                            Point startPointST = new Point(startPoint3.X, startPoint3.Y, startPoint3.Z - H);
                            Point EndPointST = new Point(startPointST.X, startPointST.Y, endPoint3.Z - EndST);

                            frame.Beam_main(AttributesTraversyvprovete[i], startPointST, EndPointST);
                        }
                    }
                    else
                    {
                        _balki.Add(frame.Beam_main(AttributesProdolnieLeft[i], startPoint, startPoint2)); // левая балка
                        _balki.Add(frame.Beam_main(AttributesProdolnieLeft[i], endPoint, endPoint2));     // правая балка  

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.Beam_main(AttributesTraversyvprovete[i], startPoint3, endPoint3)); // траверса в пролете
                        }
                    }
                }
            }
                
        }


        private void Check(int _i)
        {
            
            foreach ( Beam  B in _FraMES[0]._TraversLeft)
            {
                var M = B.StartPoint.Z;
                var M2 = _FraMES[0]._TraversRight[_i].StartPoint.Z - M;


                if (M2 <= 500 & M2  > 0)
                {
                    checkBeamR = false;
                    EndST = M2;
                    break;
                }
                else
                {
                    checkBeamR = true;
                }
            }
        }
        private void Check2(int _i)
        {
            foreach (Beam B in _FraMES[0]._TraversRight)
            {
                var M = B.StartPoint.Z;
                var M2 = _FraMES[0]._TraversLeft[_i].StartPoint.Z - M;

                if (M2 <= 500 & M2 > 0)
                {
                    checkBeamL = false;
                    EndST = M2;
                    break;
                }
                else
                {
                    checkBeamL = true;
                }
            }
        }
        public List<double> Shagtravers(double n, double Shag)
        {
            List<double> shagtravers = new List<double>();
            double a = Math.Truncate(n / Shag);
            double b = (n - (a * Shag)) / 2 + Shag/2;

            if (n > Shag)
            {
                if (Math.IEEERemainder(n, Shag) == 0)
                {
                    b = Shag;
                    shagtravers.Add(b);
                }
                else
                {
                    shagtravers.Add(b);
                }

                while (b < n - Shag)
                {
                    b += Shag;
                    shagtravers.Add(b);
                }
            }
            return shagtravers;
        }
    }
}

