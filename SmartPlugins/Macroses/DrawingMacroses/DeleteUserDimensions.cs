using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartMacros.DrawingsMacros
{
    public class DeleteUserDimensions
    {
        public static void Run()
        {
            DeleteDimensions();
        }
        private static void DeleteDimensions()
        {
            var drawingHandler = new DrawingHandler();
            if (!drawingHandler.GetConnectionStatus()) return;

            var picker = drawingHandler.GetPicker();
            drawingHandler.GetDrawingObjectSelector();

            while (true)
            {
                ViewBase view;
                DrawingObject pickedView;

                picker.PickObject("Pick view!", out pickedView, out view);

                if (view == null || pickedView == null) break;

                var dimensions = view.GetAllObjects();

                while(dimensions.MoveNext())
                {
                    if(dimensions.Current is DimensionBase dimension)
                    {
                        dimension.Delete();
                    }
                }
            }
        }
    }
}
