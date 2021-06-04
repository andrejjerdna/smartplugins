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
        AttributesFrameProlet _AttFrameProlet;
        Frame  _Frame1;
        Frame  _Frame2;

        private bool checkBeamR = true;
        private bool checkBeamL = true;

        public List<Beam> _balki = new List<Beam>();
        public List<Beam> _balkiLeft = new List<Beam>();
        public List<Beam> _traversyvprovete = new List<Beam>();
        public List<Beam> _traversyvproveteLeft = new List<Beam>();
        public List<Beam> _stoiki = new List<Beam>();

        public List<SuperProdolnayaBalka> _balkiRight = new List<SuperProdolnayaBalka>();
        public List<SuperProdolnayaBalka> _balkiLeft1 = new List<SuperProdolnayaBalka>();
        public List<SuperTraversaVProlete> _traversyVprovete = new List<SuperTraversaVProlete>();

        double EndST = 0;

        public BalkiYarysa(Frame Frame1, Frame Frame2, AttributesFrameProlet AttFrameProlet)
        {
            _Frame1 = Frame1;
            _Frame2 = Frame2;
            _AttFrameProlet = AttFrameProlet;
        }
        public BalkiYarysa()
        {
            
        }

        public void Insert()
        {
            //_balkiRight = StroimProdolnieBalki(_Frame1._TraversRight, _Frame2._TraversRight, _AttFrameProlet.AttProletBeamRight); //левые балки все
            //_balkiLeft1 = StroimProdolnieBalki(_Frame1._TraversLeft, _Frame2._TraversLeft, _AttFrameProlet.AttProletBeamLeft);    // правые балки все

            //for(int i = 0; i < _Frame1._TraversRight.Count(); i+=2)
            //{
            //    StroimTraversyVProvete(_balkiRight[i]._beam, _balkiRight[i + 1]._beam, _AttFrameProlet.AttProletTraversaRight[i]);
            //    StroimTraversyVProvete(_balkiLeft1[i]._beam, _balkiLeft1[i + 1]._beam, _AttFrameProlet.AttProletTraversaLeft[i]);
            //}



            Connections Con = new Connections();
            for (int i = 0; i < _Frame1._TraversRight.Count(); i++) // правые балки половина балок
            {
                if (_AttFrameProlet.AttProletBeamRight[i] != null)
                {
                    var Att = _Frame1._TraversRight[i];
                    var startPoint = new Point(_Frame1._basePoint.X, Att.StartPoint.Y, Att.StartPoint.Z);
                    var endPoint = new Point(_Frame1._basePoint.X, Att.EndPoint.Y, Att.EndPoint.Z);

                    var Att2 = _Frame2._TraversRight[i];
                    var startPoint2 = new Point(_Frame2._basePoint.X, Att2.StartPoint.Y, Att2.StartPoint.Z);
                    var endPoint2 = new Point(_Frame2._basePoint.X, Att2.EndPoint.Y, Att2.EndPoint.Z);

                    var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_Frame2._basePoint.X - _Frame1._basePoint.X);
                    var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);
                    Frame frame = new Frame();

                    Check(i);

                    if (!checkBeamR)
                    {
                        _balki.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamRight[i], startPoint, startPoint2, (i + 1).ToString())); // левая балка
                        checkBeamL = true;

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.BeamMain(_AttFrameProlet.AttProletTraversaRight[i], startPoint3, endPoint3, (i + 1).ToString())); // траверса в пролете
                            double H = 0;
                            _traversyvprovete[_i].GetReportProperty("WIDTH", ref H);
                            Point startPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - H);
                            Point EndPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - EndST);


                            _stoiki.Add(frame.BeamMain(_AttFrameProlet.AttProletStoyki[i], startPointST, EndPointST, (i + 1).ToString()));
                        }
                    }
                    else
                    {
                        _balki.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamRight[i], startPoint, startPoint2, (i + 1).ToString())); // левая балка
                        _balkiLeft.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamRight[i], endPoint, endPoint2, (i + 1).ToString()));     // правая балка  

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvprovete.Add(frame.BeamMain(_AttFrameProlet.AttProletTraversaRight[i], startPoint3, endPoint3, (i + 1).ToString())); // траверса в пролете
                        }
                    }
                }

                Con.BeamsToColumn(_Frame1._Columns[0]._beam, _balki);
                Con.BeamsToColumn(_Frame1._Columns[1]._beam, _balkiLeft);
            }


            for (int i = 0; i < _Frame1._TraversLeft.Count(); i++) // левые балки половина балок
            {
                if (_AttFrameProlet.AttProletBeamLeft[i] != null)
                {
                    var Att = _Frame1._TraversLeft[i];
                    var startPoint = new Point(_Frame1._basePoint.X, Att.StartPoint.Y, Att.StartPoint.Z);
                    var endPoint = new Point(_Frame1._basePoint.X, Att.EndPoint.Y, Att.EndPoint.Z);

                    var Att2 = _Frame2._TraversLeft[i];
                    var startPoint2 = new Point(_Frame2._basePoint.X, Att2.StartPoint.Y, Att2.StartPoint.Z);
                    var endPoint2 = new Point(_Frame2._basePoint.X, Att2.EndPoint.Y, Att2.EndPoint.Z);

                    var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_Frame2._basePoint.X - _Frame1._basePoint.X);
                    var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);
                    Frame frame = new Frame();

                    Check2(i);

                    if (!checkBeamL)
                    {
                        _balkiLeft.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamLeft[i], endPoint, endPoint2, (i + 1).ToString()));     // правая балка
                        checkBeamR = true;
                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvproveteLeft.Add(frame.BeamMain(_AttFrameProlet.AttProletTraversaLeft[i], startPoint3, endPoint3, (i + 1).ToString())); // траверса в пролете
                            double H = 0;
                            _traversyvproveteLeft[_i].GetReportProperty("WIDTH", ref H);
                            Point startPointST = new Point(startPoint3.X, startPoint3.Y, startPoint3.Z - H);
                            Point EndPointST = new Point(startPointST.X, startPointST.Y, endPoint3.Z - EndST);

                            _stoiki.Add(frame.BeamMain(_AttFrameProlet.AttProletStoyki[i], startPointST, EndPointST, (i + 1).ToString()));
                        }
                    }
                    else
                    {
                        _balki.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamLeft[i], startPoint, startPoint2, (i + 1).ToString())); // левая балка
                        _balkiLeft.Add(frame.BeamMain(_AttFrameProlet.AttProletBeamLeft[i], endPoint, endPoint2, (i + 1).ToString()));     // правая балка  

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);
                            _traversyvproveteLeft.Add(frame.BeamMain(_AttFrameProlet.AttProletTraversaLeft[i], startPoint3, endPoint3, (i + 1).ToString())); // траверса в пролете
                        }
                    }
                }
                Con.BeamsToColumn(_Frame1._Columns[0]._beam, _balki);
                Con.BeamsToColumn(_Frame1._Columns[1]._beam, _balkiLeft);

            }
        }

        private List<SuperProdolnayaBalka> StroimProdolnieBalki(List<Beam> beams1, List<Beam> beams2, List<Attributes> attributes)
        {
            List<SuperProdolnayaBalka> balki = new List<SuperProdolnayaBalka>();
             for (int i = 0; i < beams1.Count(); i++) // правые балки половина балок
            {
                if (attributes[i] != null)
                {
                    var startPoint = new Point(_Frame1._basePoint.X, beams1[i].StartPoint.Y, beams1[i].StartPoint.Z);
                    var endPoint = new Point(_Frame1._basePoint.X, beams1[i].EndPoint.Y, beams1[i].EndPoint.Z);

                    var startPoint2 = new Point(_Frame2._basePoint.X, beams2[i].StartPoint.Y, beams2[i].StartPoint.Z);
                    var endPoint2 = new Point(_Frame2._basePoint.X, beams2[i].EndPoint.Y, beams2[i].EndPoint.Z);

                    SuperProdolnayaBalka pbalka1 = new SuperProdolnayaBalka(attributes[i], startPoint, endPoint);
                    pbalka1.Insert();
                    balki.Add(pbalka1);
                    SuperProdolnayaBalka pbalka2 = new SuperProdolnayaBalka(attributes[i], startPoint2, endPoint2);
                    pbalka2.Insert();
                    balki.Add(pbalka2);
                }
            }
            return balki;
        }

        private void StroimTraversyVProvete(Beam Right, Beam Left, Attributes att)
        {
            var uklon = (Right.EndPoint.Z - Right.StartPoint.Z) / (_Frame2._basePoint.X - _Frame1._basePoint.X);
            var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);
            for (int _i = 0; _i < _shagi.Count(); _i++)
            {
                Point startPoint = new Point(Right.StartPoint.X + _shagi[_i], Right.StartPoint.Y, Right.StartPoint.Z + _shagi[_i] * uklon);
                Point endPoint = new Point(Left.EndPoint.X + _shagi[_i], Left.EndPoint.Y, Left.EndPoint.Z + _shagi[_i] * uklon);

                SuperTraversaVProlete traversaVprolete = new SuperTraversaVProlete(att, startPoint, endPoint);
                traversaVprolete.Insert();


               // _traversyvprovete.Add(frame.BeamMain(_AttFrameProlet.AttProletTraversaRight[i], startPoint3, endPoint3, (i + 1).ToString())); // траверса в пролете
            }
        }


        private void Check(int _i)
        {
            
            foreach ( Beam  B in _Frame1._TraversLeft)
            {
                var M = B.StartPoint.Z;
                var M2 = _Frame1._TraversRight[_i].StartPoint.Z - M;


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
            foreach (Beam B in _Frame1._TraversRight)
            {
                var M = B.StartPoint.Z;
                var M2 = _Frame1._TraversLeft[_i].StartPoint.Z - M;

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

