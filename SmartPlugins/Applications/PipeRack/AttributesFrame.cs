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

            AttributesYarusRight = new List<Attributes>() 
                {
                    attYarusRight1,
                    attYarusRight2,
                    attYarusRight3,
                    attYarusRight4,
                    attYarusRight5,
                    attYarusRight6,
                    attYarusRight7,
                };

            AttributesYarusLeft = new List<Attributes>() 
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
}
