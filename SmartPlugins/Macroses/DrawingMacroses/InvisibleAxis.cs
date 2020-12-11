using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartMacros
{
    public class InvisibleAxis
    {
        public static void Run(string type)
        {
            InvisibleGorizontalAxis(type);
        }
        private static void InvisibleGorizontalAxis(string type)
        {
            var drawingHandler = new DrawingHandler();
            if (!drawingHandler.GetConnectionStatus()) return;

            var picker = drawingHandler.GetPicker();

            ViewBase view;
            DrawingObject pickedGrid;

            try
            {
                picker.PickObject("Pick grid!", out pickedGrid, out view);

                if (pickedGrid == null || view == null) return;

                if (pickedGrid is Grid grids)
                {
                    var gridLines = grids.GetObjects();

                    while (gridLines.MoveNext())
                    {
                        if (gridLines.Current is GridLine gridLine)
                        {
                            if (type == "Y")
                            {
                                if (gridLine.StartLabel.GridLabelPoint.Y == gridLine.EndLabel.GridLabelPoint.Y)
                                {
                                    gridLine.Attributes.Font.Color = DrawingColors.Invisible;
                                    gridLine.Attributes.Line.Color = DrawingColors.Invisible;
                                    gridLine.Modify();
                                }
                            }
                            else
                            {
                                if (gridLine.StartLabel.GridLabelPoint.X == gridLine.EndLabel.GridLabelPoint.X)
                                {
                                    gridLine.Attributes.Font.Color = DrawingColors.Invisible;
                                    gridLine.Attributes.Line.Color = DrawingColors.Invisible;
                                    gridLine.Modify();
                                }
                            }
                        }
                    }
                }

                view.Modify();
            }
            catch
            {

            }
        }
    }
}
