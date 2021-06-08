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
        public Frame _Frame1 { get; set; }
        public Frame _Frame2 { get; set; }

        private bool checkBeamR = true;
        private bool checkBeamL = true;

        //public List<SuperProdolnayaBalka> BalkiRight = new List<SuperProdolnayaBalka>();
       // public List<SuperProdolnayaBalka> BalkiLeft = new List<SuperProdolnayaBalka>();

        public List<SuperProdolnieBalkiYarusa> BalkiRightT = new List<SuperProdolnieBalkiYarusa>();
        public List<SuperProdolnieBalkiYarusa> BalkiLeftT = new List<SuperProdolnieBalkiYarusa>();

        public List<SuperTraversaVProlete> TraversyVProveteRight = new List<SuperTraversaVProlete>();
        public List<SuperTraversaVProlete> TraversyVProveteLeft = new List<SuperTraversaVProlete>();
        public List<Beam> _stoiki = new List<Beam>();

        public List<SuperTraversaVProlete> _traversyVprovete = new List<SuperTraversaVProlete>();

        public List<bool> RightZ = new List<bool>();
        public List<bool> LeftZ = new List<bool>();

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


            CheckNaxlest();

            for (int i = 0; i < _Frame1._TraversRight.Count(); i++)
            {
                var beams1 = _Frame1._TraversRight[i];
                var beams2 = _Frame2._TraversRight[i];

                var startPoint = new Point(_Frame1._basePoint.X, beams1.StartPoint.Y, beams1.StartPoint.Z);
                var endPoint = new Point(_Frame2._basePoint.X, beams2.StartPoint.Y, beams2.StartPoint.Z);

                var startPoint2 = new Point(_Frame1._basePoint.X, beams1.EndPoint.Y, beams1.EndPoint.Z);
                var endPoint2 = new Point(_Frame2._basePoint.X, beams2.EndPoint.Y, beams2.EndPoint.Z);

                SuperProdolnieBalkiYarusa balkiRight = new SuperProdolnieBalkiYarusa()
                {
                    Stroim = RightZ[i],
                    Direction = "Right",
                    StartPoint1 = startPoint,
                    StartPoint2 = startPoint2,
                    EndPoint1 = endPoint,
                    EndPoint2 = endPoint2,
                    Att = _AttFrameProlet.AttProletBeamRight[i],
                };
                balkiRight.Insert();
                BalkiRightT.Add(balkiRight);
            }

            for (int i = 0; i < _Frame1._TraversLeft.Count(); i++)
            {
                var beams1 = _Frame1._TraversLeft[i];
                var beams2 = _Frame2._TraversLeft[i];

                var startPoint = new Point(_Frame1._basePoint.X, beams1.StartPoint.Y, beams1.StartPoint.Z);
                var endPoint = new Point(_Frame2._basePoint.X, beams2.StartPoint.Y, beams2.StartPoint.Z);
                
                var startPoint2 = new Point(_Frame1._basePoint.X, beams1.EndPoint.Y, beams1.EndPoint.Z);
                var endPoint2 = new Point(_Frame2._basePoint.X, beams2.EndPoint.Y, beams2.EndPoint.Z);


                SuperProdolnieBalkiYarusa balkiLeft = new SuperProdolnieBalkiYarusa()
                {
                    Stroim = LeftZ[i],
                    Direction = "Left",
                    StartPoint1 = startPoint,
                    StartPoint2 = startPoint2,
                    EndPoint1 = endPoint,
                    EndPoint2 = endPoint2,
                    Att = _AttFrameProlet.AttProletBeamLeft[i],
                };
                balkiLeft.Insert();
                BalkiRightT.Add(balkiLeft);
            }



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

                        for (int _i = 0; _i < _shagi.Count(); _i++)
                        {
                            Point startPoint3 = new Point(startPoint.X + _shagi[_i], startPoint.Y, startPoint.Z + _shagi[_i] * uklon);
                            Point endPoint3 = new Point(endPoint.X + _shagi[_i], endPoint.Y, endPoint.Z + _shagi[_i] * uklon);

                            SuperTraversaVProlete traversavprolete = new SuperTraversaVProlete(_AttFrameProlet.AttProletTraversaRight[i], startPoint3, endPoint3);// траверса в пролете
                            traversavprolete.Insert();
                            TraversyVProveteRight.Add(traversavprolete);

                            double H = 0;
                            TraversyVProveteRight[_i]._beam.GetReportProperty("WIDTH", ref H);
                            Point startPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - H);
                            Point EndPointST = new Point(endPoint3.X, endPoint3.Y, endPoint3.Z - EndST);


                            _stoiki.Add(frame.BeamMain(_AttFrameProlet.AttProletStoyki[i], startPointST, EndPointST, (i + 1).ToString()));
                        }
                   
                }

              //  Con.BeamsToColumn(_Frame1._Columns[0]._beam, BalkiRight);
              //  Con.BeamsToColumn(_Frame1._Columns[1]._beam, BalkiLeft);
            }

        }

        public void Modify()
        {

            foreach (SuperProdolnayaBalka balka in BalkiLeft)
            {
                balka.StartPoint.X = _Frame1._basePoint.X;
                balka.EndPoint.X = _Frame2._basePoint.X;

                balka.Modify();
            }
            foreach (SuperProdolnayaBalka balka in BalkiRight)
            {
                balka.StartPoint.X = _Frame1._basePoint.X;
                balka.EndPoint.X = _Frame2._basePoint.X;
                balka.Modify();
            }
            foreach (SuperTraversaVProlete traversa in TraversyVProveteLeft)
            {
                //var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);
                //var uklon = (Att2.EndPoint.Z - Att.StartPoint.Z) / (_Frame2._basePoint.X - _Frame1._basePoint.X);


                //traversa.StartPoint.X  = _shagi[_i];
                //traversa.EndPoint.X = _shagi[_i];

                traversa.Modify();
            }
            foreach (SuperTraversaVProlete traversa in TraversyVProveteRight)
            {
                traversa.Modify();
            }
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


        private void CheckNaxlest()
        {
            
            foreach (SuperTraversaYarysa beamsRight in _Frame1._TraversRight)
            {
                bool nadoLiStroitb = true; 
                var startZRight = beamsRight.StartPoint.Z;

                foreach(SuperTraversaYarysa beamsLeft in _Frame1._TraversLeft)
                {
                    var startZLeft = beamsLeft._beam.StartPoint.Z;

                    if ( (startZRight - startZLeft) <= 500 & (startZRight - startZLeft) > 0)
                        nadoLiStroitb = false;
                }

            if (!nadoLiStroitb)
                    RightZ.Add(false);
            else
                    RightZ.Add(true);
            }

            foreach (SuperTraversaYarysa beamsLeft in _Frame1._TraversLeft)
            {
                bool nadoLiStroitb = true;
                var startZLeft = beamsLeft.StartPoint.Z;
                
                foreach (SuperTraversaYarysa beamsRight in _Frame1._TraversRight)
                {
                    var startZRight = beamsRight._beam.StartPoint.Z;

                    if ((startZLeft - startZRight) <= 500 & (startZLeft - startZRight) > 0)
                        nadoLiStroitb = false;
                }
                if (!nadoLiStroitb)
                    LeftZ.Add(false);
               
                else
                    LeftZ.Add(true);
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

