using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartMacros.DrawingsMacros
{
    public class ReopenDrawing
    {
        public static void Run()
        {
            try
            {
                ReopenDraw();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + ex.StackTrace);
            }
        }
        private static void ReopenDraw()
        {
            var drawingHandler = new DrawingHandler();
            var activeDrawing = drawingHandler.GetActiveDrawing();

            if (activeDrawing != null)
            {
                drawingHandler.SaveActiveDrawing();
                drawingHandler.SetActiveDrawing(activeDrawing, true);
                return;
            }
            Tekla.Structures.Model.Operations.Operation.DisplayPrompt("No drawing found, macro failed.");
        }
    }
}
