using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeRack
{
    class PlatformMaintenance
    {
        List<Frame> _FraMES;
        public PlatformMaintenance(List<Frame> FraMES)
        {
            _FraMES = FraMES;
        }

        public void Insert()
        {
            Frame frame = new Frame();

            var startColumn = _FraMES[0]._Columns.First(); //последняя колонна
            var lastColumn = _FraMES[0]._Columns.Last(); //последняя колонна





        }

    }
}
