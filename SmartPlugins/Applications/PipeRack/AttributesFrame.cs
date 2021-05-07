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

        public Attributes attLeftYarus1;
        public Attributes attLeftYarus2;
        public Attributes attLeftYarus3;
        public Attributes attLeftYarus4;
        public Attributes attLeftYarus5;
        public Attributes attLeftYarus6;
        public Attributes attLeftYarus7;

        public List<Attributes> _attributesColumn { get; set; }
        public void Listovod()
        {
            _attributesColumn = new List<Attributes>() // колонны
                {
                    attColumn1, attColumn2, attColumn3,
                };
        }

    }

    class AttributesFrameProlet
    {
        private Attributes attRightYarus1 { get; set; }
        private Attributes attRightYarus2 { get; set; }
        private Attributes attRightYarus3 { get; set; }
        private Attributes attRightYarus4 { get; set; }
        private Attributes attRightYarus5 { get; set; }
        private Attributes attRightYarus6 { get; set; }
        private Attributes attRightYarus7 { get; set; }


        private Attributes attLeftYarus1 { get; set; }
        private Attributes attLeftYarus2 { get; set; }
        private Attributes attLeftYarus3 { get; set; }
        private Attributes attLeftYarus4 { get; set; }
        private Attributes attLeftYarus5 { get; set; }
        private Attributes attLeftYarus6 { get; set; }
        private Attributes attLeftYarus7 { get; set; }

    }
}
