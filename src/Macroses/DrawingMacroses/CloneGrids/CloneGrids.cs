using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace SmartMacros.DrawingsMacros
{
    public class CloneGrids
    {
        public static void Run()
        {
            var form = new CloneGridsForm();
            form.ShowDialog();
        }
        public static List<Grid> PickGrids()
        {
            var drawingHandler = new DrawingHandler();
            if (!drawingHandler.GetConnectionStatus()) return null;

            var result = new List<Grid>();

            var picker = drawingHandler.GetPicker();

            try
            {
                while (true)
                {
                    ViewBase view;
                    DrawingObject pickedGrid;

                    picker.PickObject("Pick grid!", out pickedGrid, out view);

                    if (pickedGrid == null || view == null) break;

                    if (pickedGrid is Grid grids)
                    {
                        result.Add(grids);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {

            }

            return result;
        }
        public static void CloneParamGrids(List<Grid> mainGrids, List<Grid> secondaryGrids)
        {
            foreach (Grid mainGrid in mainGrids)
            {
                foreach (Grid secondaryGrid in secondaryGrids)
                {
                    if (secondaryGrid.ModelIdentifier.ID == mainGrid.ModelIdentifier.ID)
                    {
                        var gridLines1 = mainGrid.GetObjects().ToIEnumerable<GridLine>().ToList();
                        var gridLines2 = secondaryGrid.GetObjects().ToIEnumerable<GridLine>().ToList();

                        foreach(GridLine gridLine1 in gridLines1)
                        {
                            foreach (GridLine gridLine2 in gridLines2)
                            {
                                if (gridLine2.ModelIdentifier.ID == gridLine1.ModelIdentifier.ID)
                                {
                                    //TODO: Подумать над шибкой наезжания оси на кружлк оси.
                                    gridLine2.Select();
                                    gridLine2.Attributes = gridLine1.Attributes;
                                    gridLine2.StartLabel = gridLine1.StartLabel;
                                    gridLine2.EndLabel = gridLine1.EndLabel;
                                    gridLine2.Modify();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
