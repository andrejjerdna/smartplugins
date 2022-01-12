using SmartPlugins.Common.Abstractions;
using SmartPlugins.Common.TeklaLibrary;
using tsd = Tekla.Structures.Drawing;
using tsg = Tekla.Structures.Geometry3d;

namespace SmartPlugins.Macros.Library
{
    public class EmbededMark : ITeklaMacro
    {
        public void RunLoop()
        {
            throw new System.NotImplementedException();
        }

        public void RunOnce()
        {
            var smartModel = new SmartModel();
			
            if (!smartModel.ConnectionStatus)
				return;

			var drawingHandler = new tsd.DrawingHandler();
			var activeDrawing = drawingHandler.GetActiveDrawing();
			var selectedDrawingObjects = drawingHandler.GetDrawingObjectSelector().GetSelected();

			var picker = drawingHandler.GetPicker();
			tsd.ViewBase viewBase = null;
			var newPoint = new tsg.Point(0, 0, 0);
			picker.PickPoint("", out newPoint, out viewBase);

			while (selectedDrawingObjects.MoveNext())
			{
				if (selectedDrawingObjects.Current is tsd.Mark)
				{
					tsd.Mark mark = selectedDrawingObjects.Current as tsd.Mark;
					mark.InsertionPoint = newPoint;
					mark.Modify();
					mark.InsertionPoint = newPoint;
					mark.Modify();
				}
			}
		}
	}
}
