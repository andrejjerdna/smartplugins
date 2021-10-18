using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.SmartTeklaModel;

using tsd = Tekla.Structures.Drawing;
using tsg = Tekla.Structures.Geometry3d;
using Tekla.Structures.Drawing.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPlugins.Macroses.Library
{
    public class EmbededMark : ITeklaMacro
    {
        public void Run()
        {
            var model = new SmartModel();
			
            if (!model.ConnectionStatus)
                return;
			// to do smart picker drawing
			tsd.DrawingHandler dh = new tsd.DrawingHandler();
			tsd.Drawing draw = dh.GetActiveDrawing();

			tsd.DrawingObjectEnumerator doe = dh.GetDrawingObjectSelector().GetSelected();

			tsd.UI.Picker picker = dh.GetPicker();
			tsd.ViewBase vb = null;
			tsg.Point p = new tsg.Point(0, 0, 0);
			picker.PickPoint("", out p, out vb);

			while (doe.MoveNext())
			{
				if (doe.Current is tsd.Mark)
				{

					tsd.Mark m = doe.Current as tsd.Mark;
					m.InsertionPoint = p;
					m.Modify();
					m.InsertionPoint = p;
					m.Modify();

				}
			}
		}
	}
}
