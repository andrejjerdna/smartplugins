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

        public List<SuperProdolnayaBalka> BalkiRight = new List<SuperProdolnayaBalka>(); //удалить позже
        public List<SuperProdolnayaBalka> BalkiLeft = new List<SuperProdolnayaBalka>();  //удалить позже

        public List<SuperProdolnieBalkiYarusa> BalkiRightT = new List<SuperProdolnieBalkiYarusa>();
        public List<SuperProdolnieBalkiYarusa> BalkiLeftT = new List<SuperProdolnieBalkiYarusa>();

        public List<SuperTraversaVProlete> TraversyVProveteRight = new List<SuperTraversaVProlete>();  //удалить позже
        public List<SuperTraversaVProlete> TraversyVProveteLeft = new List<SuperTraversaVProlete>();  //удалить позже

        public List<SuperTraversyVProleteYarysa> TraversyVProveteRightT = new List<SuperTraversyVProleteYarysa>();
        public List<SuperTraversyVProleteYarysa> TraversyVProveteLeftT = new List<SuperTraversyVProleteYarysa>();

        public List<Beam> _stoiki = new List<Beam>();

        public List<SuperTraversaVProlete> _traversyVprovete = new List<SuperTraversaVProlete>();

        public List<bool> RightZ = new List<bool>();
        public List<bool> LeftZ = new List<bool>();

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

            BalkiRightT = PostroenieProdolnihBalok("Right");
            BalkiLeftT = PostroenieProdolnihBalok("Left");

            TraversyVProveteRightT = PostroenieTravers("Right");
            TraversyVProveteLeftT = PostroenieTravers("Left");

            var ii = 0;
            foreach (bool boolevo in RightZ)
            {
                if (!boolevo)
                {
                    SuperStoikiVProlete stoiki = new SuperStoikiVProlete(TraversyVProveteRightT[ii])
                    {
                        Att = _AttFrameProlet.AttProletStoyki[ii],
                    };
                    stoiki.Insert();
                    ii++;
                }
            }
            var kk = 0;
            foreach (bool boolevo in LeftZ)
            {
                if (!boolevo)
                {
                    SuperStoikiVProlete stoiki = new SuperStoikiVProlete(TraversyVProveteLeftT[kk])
                    {
                        Att = _AttFrameProlet.AttProletStoyki[kk],
                    };
                    stoiki.Insert();
                    kk++;
                }
            }



            //     Connections Con = new Connections();
            //    Con.BeamsToColumn(_Frame1._Columns[0]._beam, BalkiRight);
            //    Con.BeamsToColumn(_Frame1._Columns[1]._beam, BalkiLeft);
            //

        }

        public void Modify()
        {

            foreach (SuperProdolnieBalkiYarusa balkiProdolnie in BalkiRightT)
            {
                balkiProdolnie.StartPoint1.X = _Frame1._basePoint.X;
                balkiProdolnie.StartPoint2.X = _Frame1._basePoint.X;
                balkiProdolnie.EndPoint1.X = _Frame2._basePoint.X;
                balkiProdolnie.EndPoint2.X = _Frame2._basePoint.X;
                balkiProdolnie.Modify();
            }
            foreach (SuperProdolnieBalkiYarusa balkiProdolnie in BalkiLeftT)
            {
                balkiProdolnie.StartPoint1.X = _Frame1._basePoint.X;
                balkiProdolnie.StartPoint2.X = _Frame1._basePoint.X;
                balkiProdolnie.EndPoint1.X = _Frame2._basePoint.X;
                balkiProdolnie.EndPoint2.X = _Frame2._basePoint.X;
                balkiProdolnie.Modify();
            }
            foreach(SuperTraversyVProleteYarysa traversyVProlete in TraversyVProveteRightT)
            {
                var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);
                traversyVProlete.Shagi = _shagi;
                traversyVProlete.StartPoint.X = _Frame1._basePoint.X;
                traversyVProlete.EndPoint.X = _Frame1._basePoint.X;
                traversyVProlete.Modify();
            }

        }

        public void Delete()
        {
            foreach (SuperProdolnieBalkiYarusa balkiProdolnie in BalkiRightT)
            {

                balkiProdolnie.Delete();
            }
            foreach (SuperProdolnieBalkiYarusa balkiProdolnie in BalkiLeftT)
            {

                balkiProdolnie.Delete();
            }
            foreach (SuperTraversyVProleteYarysa traversyVProlete in TraversyVProveteRightT)
            {

                traversyVProlete.Delete();
            }
        }


        private List<SuperProdolnieBalkiYarusa> PostroenieProdolnihBalok(String direction)
        {
            List<SuperProdolnieBalkiYarusa> sBalki = new List<SuperProdolnieBalkiYarusa>();
            List<SuperTraversaYarysa> beams1 = _Frame1._TraversRight;
            List<SuperTraversaYarysa> beams2 = _Frame2._TraversRight;
            List<Attributes> attributes = _AttFrameProlet.AttProletBeamRight;
            List<bool> stroim = RightZ;

            if (direction == "Left")
            {
                beams1 = _Frame1._TraversLeft;
                beams2 = _Frame2._TraversLeft;
                attributes = _AttFrameProlet.AttProletBeamLeft;
                stroim = LeftZ;
            }

            for (int i = 0; i < beams1.Count(); i++)
            {
                var startPoint = new Point(_Frame1._basePoint.X, beams1[i].StartPoint.Y, beams1[i].StartPoint.Z);
                var endPoint = new Point(_Frame2._basePoint.X, beams2[i].StartPoint.Y, beams2[i].StartPoint.Z);

                var startPoint2 = new Point(_Frame1._basePoint.X, beams1[i].EndPoint.Y, beams1[i].EndPoint.Z);
                var endPoint2 = new Point(_Frame2._basePoint.X, beams2[i].EndPoint.Y, beams2[i].EndPoint.Z);

                SuperProdolnieBalkiYarusa balki = new SuperProdolnieBalkiYarusa()
                {
                    Stroim = stroim[i],
                    Direction = direction,
                    StartPoint1 = startPoint,
                    StartPoint2 = startPoint2,
                    EndPoint1 = endPoint,
                    EndPoint2 = endPoint2,
                    Att = attributes[i],
                };
                balki.Insert();
                sBalki.Add(balki);
            }
            return sBalki;
        }
        private List<SuperTraversyVProleteYarysa> PostroenieTravers (String direction)
        {
            List<SuperTraversyVProleteYarysa> traversyYarysa = new List<SuperTraversyVProleteYarysa>();
            var balki = BalkiRightT;
            if (direction == "Left")
            {
                balki = BalkiLeftT;
            }
            var _shagi = Shagtravers(_Frame2._basePoint.X - _Frame1._basePoint.X, 3000);

            int i = 0;
            foreach (SuperProdolnieBalkiYarusa beams in balki)
            {

                var uklon = (beams.EndPoint1.Z - beams.StartPoint1.Z) / (_Frame2._basePoint.X - _Frame1._basePoint.X);
                SuperTraversyVProleteYarysa traversyVprolete = new SuperTraversyVProleteYarysa(_shagi, uklon)
                {
                    Att = _AttFrameProlet.AttProletTraversaRight[i],
                    StartPoint = beams.StartPoint1,
                    EndPoint = beams.StartPoint2,
                };
                traversyVprolete.Insert();
                traversyYarysa.Add(traversyVprolete);
                i++;
            }
            return traversyYarysa;
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

