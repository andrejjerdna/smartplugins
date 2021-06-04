using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeRack
{
    class AttributesFrameProlet
    {
        public List<Attributes> AttProletBeamRight { get; set; }
        public List<Attributes> AttProletBeamLeft { get; set; }
        public List<Attributes> AttProletTraversaRight { get; set; }
        public List<Attributes> AttProletTraversaLeft { get; set; }
        public List<Attributes> AttProletStoyki { get; set; }

        public AttributesFrameProlet()
        {
            Attributes attProletBeamRight1 = null ;
             Attributes attProletBeamRight2 = null;
             Attributes attProletBeamRight3 = null;
             Attributes attProletBeamRight4 = null;
             Attributes attProletBeamRight5 = null;
             Attributes attProletBeamRight6 = null;
             Attributes attProletBeamRight7 = null;

             Attributes attProletBeamLeft1 = null;
             Attributes attProletBeamLeft2 = null;
             Attributes attProletBeamLeft3 = null;
             Attributes attProletBeamLeft4 = null;
             Attributes attProletBeamLeft5 = null;
             Attributes attProletBeamLeft6 = null;
             Attributes attProletBeamLeft7 = null;

             Attributes attProletTraversaRight1 = null;
             Attributes attProletTraversaRight2 = null;
             Attributes attProletTraversaRight3 = null;
             Attributes attProletTraversaRight4 = null;
             Attributes attProletTraversaRight5 = null;
             Attributes attProletTraversaRight6 = null;
             Attributes attProletTraversaRight7 = null;

             Attributes attProletTraversaLeft1 = null;
             Attributes attProletTraversaLeft2 = null;
             Attributes attProletTraversaLeft3 = null;
             Attributes attProletTraversaLeft4 = null;
             Attributes attProletTraversaLeft5 = null;
             Attributes attProletTraversaLeft6 = null;
             Attributes attProletTraversaLeft7 = null;

             Attributes attProletStoyki1 = null;
             Attributes attProletStoyki2 = null;
             Attributes attProletStoyki3 = null;
             Attributes attProletStoyki4 = null;
             Attributes attProletStoyki5 = null;
             Attributes attProletStoyki6 = null;
             Attributes attProletStoyki7 = null;

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
