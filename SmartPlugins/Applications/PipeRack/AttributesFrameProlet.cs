using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeRack
{
    class AttributesFrameProlet
    {
        public Attributes attProletBeamRight1;
        public Attributes attProletBeamRight2;
        public Attributes attProletBeamRight3;
        public Attributes attProletBeamRight4;
        public Attributes attProletBeamRight5;
        public Attributes attProletBeamRight6;
        public Attributes attProletBeamRight7;

        public Attributes attProletBeamLeft1;
        public Attributes attProletBeamLeft2;
        public Attributes attProletBeamLeft3;
        public Attributes attProletBeamLeft4;
        public Attributes attProletBeamLeft5;
        public Attributes attProletBeamLeft6;
        public Attributes attProletBeamLeft7;

        public Attributes attProletTraversaRight1;
        public Attributes attProletTraversaRight2;
        public Attributes attProletTraversaRight3;
        public Attributes attProletTraversaRight4;
        public Attributes attProletTraversaRight5;
        public Attributes attProletTraversaRight6;
        public Attributes attProletTraversaRight7;

        public Attributes attProletTraversaLeft1;
        public Attributes attProletTraversaLeft2;
        public Attributes attProletTraversaLeft3;
        public Attributes attProletTraversaLeft4;
        public Attributes attProletTraversaLeft5;
        public Attributes attProletTraversaLeft6;
        public Attributes attProletTraversaLeft7;

        public Attributes attProletStoyki1;
        public Attributes attProletStoyki2;
        public Attributes attProletStoyki3;
        public Attributes attProletStoyki4;
        public Attributes attProletStoyki5;
        public Attributes attProletStoyki6;
        public Attributes attProletStoyki7;

        public List<Attributes> AttProletBeamRight { get; set; }

        public List<Attributes> AttProletBeamLeft { get; set; }
        public List<Attributes> AttProletTraversaRight { get; set; }
        public List<Attributes> AttProletTraversaLeft { get; set; }
        public List<Attributes> AttProletStoyki { get; set; }

        public void Listovod()
        {
            AttProletBeamRight = new List<Attributes>()
                {
                    attProletBeamRight1,
                    attProletBeamRight2,
                    attProletBeamRight3,
                    attProletBeamRight4,
                    attProletBeamRight5,
                    attProletBeamRight6,
                    attProletBeamRight7,
                };

            AttProletBeamLeft = new List<Attributes>()
                {
                    attProletBeamLeft1,
                    attProletBeamLeft2,
                    attProletBeamLeft3,
                    attProletBeamLeft4,
                    attProletBeamLeft5,
                    attProletBeamLeft6,
                    attProletBeamLeft7,
                };

            AttProletTraversaRight = new List<Attributes>()
                {
                    attProletTraversaRight1,
                    attProletTraversaRight2,
                    attProletTraversaRight3,
                    attProletTraversaRight4,
                    attProletTraversaRight5,
                    attProletTraversaRight6,
                    attProletTraversaRight7,
                };

            AttProletTraversaLeft = new List<Attributes>()
                {
                    attProletTraversaLeft1,
                    attProletTraversaLeft2,
                    attProletTraversaLeft3,
                    attProletTraversaLeft4,
                    attProletTraversaLeft5,
                    attProletTraversaLeft6,
                    attProletTraversaLeft7,
                };
            AttProletStoyki = new List<Attributes>()
            {
                attProletStoyki1,
                attProletStoyki2,
                attProletStoyki3,
                attProletStoyki4,
                attProletStoyki5,
                attProletStoyki6,
                attProletStoyki7,
            };
        }
    }
}
