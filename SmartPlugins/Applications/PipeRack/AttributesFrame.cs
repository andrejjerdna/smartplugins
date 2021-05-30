using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeRack
{
    public class AttributesFrame
    {
        public List<Attributes> AttributesColumn { get; set; }
        public List<Attributes> AttributesYarusRight { get; set; }
        public List<Attributes> AttributesYarusLeft { get; set; }
        public void Listovod()
        {
            Attributes attColumn1 = null;
            Attributes attColumn2 = null;
            Attributes attColumn3 = null;

            Attributes attYarusRight1 = null;
            Attributes attYarusRight2 = null;
            Attributes attYarusRight3 = null;
            Attributes attYarusRight4 = null;
            Attributes attYarusRight5 = null;
            Attributes attYarusRight6 = null;
            Attributes attYarusRight7 = null;

            Attributes attYarusLeft1 = null;
            Attributes attYarusLeft2 = null;
            Attributes attYarusLeft3 = null;
            Attributes attYarusLeft4 = null;
            Attributes attYarusLeft5 = null;
            Attributes attYarusLeft6 = null;
            Attributes attYarusLeft7 = null;

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
