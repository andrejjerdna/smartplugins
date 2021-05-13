using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeRack
{
    public class AttributesFrame
    {
        public Attributes attColumn1;
        public Attributes attColumn2;
        public Attributes attColumn3;

        public Attributes attYarusRight1;
        public Attributes attYarusRight2;
        public Attributes attYarusRight3;
        public Attributes attYarusRight4;
        public Attributes attYarusRight5;
        public Attributes attYarusRight6;
        public Attributes attYarusRight7;

        public Attributes attYarusLeft1;
        public Attributes attYarusLeft2;
        public Attributes attYarusLeft3;
        public Attributes attYarusLeft4;
        public Attributes attYarusLeft5;
        public Attributes attYarusLeft6;
        public Attributes attYarusLeft7;

        public List<Attributes> AttributesColumn { get; set; }
        public List<Attributes> AttributesYarusRight { get; set; }
        public List<Attributes> AttributesYarusLeft { get; set; }
        public void Listovod()
        {
            AttributesColumn = new List<Attributes>() // колонны
                {
                    attColumn1,
                    attColumn2,
                    attColumn3,
                };

            AttributesYarusRight = new List<Attributes>() // колонны
                {
                    attYarusRight1,
                    attYarusRight2,
                    attYarusRight3,
                    attYarusRight4,
                    attYarusRight5,
                    attYarusRight6,
                    attYarusRight7,
                };

            AttributesYarusLeft = new List<Attributes>() // колонны
                {
                    attYarusLeft1,
                    attYarusLeft2,
                    attYarusLeft3,
                    attYarusLeft4,
                    attYarusLeft5,
                    attYarusLeft6,
                    attYarusLeft7,
                };
        }

    }

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

        public List<Attributes> AttProletBeamRight { get; set; }
        public List<Attributes> AttProletBeamLeft { get; set; }
        public List<Attributes> attProletTraversaRight { get; set; }
        public List<Attributes> attProletTraversaLeft { get; set; }

        public void Listovod()
        {
            AttProletBeamRight = new List<Attributes>() // колонны
                {
                    attProletBeamRight1,
                    attProletBeamRight2,
                    attProletBeamRight3,
                    attProletBeamRight4,
                    attProletBeamRight5,
                    attProletBeamRight6,
                    attProletBeamRight7,
                };

            AttProletBeamLeft = new List<Attributes>() // колонны
                {
                    attProletBeamLeft1,
                    attProletBeamLeft2,
                    attProletBeamLeft3,
                    attProletBeamLeft4,
                    attProletBeamLeft5,
                    attProletBeamLeft6,
                    attProletBeamLeft7,
                };

            attProletTraversaRight = new List<Attributes>() // колонны
                {
                    attProletTraversaRight1,
                    attProletTraversaRight2,
                    attProletTraversaRight3,
                    attProletTraversaRight4,
                    attProletTraversaRight5,
                    attProletTraversaRight6,
                    attProletTraversaRight7,
                };

            attProletTraversaLeft = new List<Attributes>() // колонны
                {
                    attProletTraversaLeft1,
                    attProletTraversaLeft2,
                    attProletTraversaLeft3,
                    attProletTraversaLeft4,
                    attProletTraversaLeft5,
                    attProletTraversaLeft6,
                    attProletTraversaLeft7,
                };
        }
    }
}
