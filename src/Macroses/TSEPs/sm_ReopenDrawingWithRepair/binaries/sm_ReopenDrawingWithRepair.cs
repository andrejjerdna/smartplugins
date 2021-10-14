using System;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model.Operations;

#pragma warning disable 1633 // Unrecognized #pragma directive
#pragma reference "Tekla.Macros.Wpf.Runtime"
#pragma reference "Tekla.Macros.Akit"
#pragma reference "Tekla.Macros.Runtime"
#pragma warning restore 1633 // Unrecognized #pragma directive

namespace Tekla.Technology.Akit.UserScript
{
    public sealed class Macro
    {
        [Tekla.Macros.Runtime.MacroEntryPointAttribute()]
        public static void Run(Tekla.Macros.Runtime.IMacroRuntime runtime)
        {
            var drawingHandler = new DrawingHandler();
            var activeDrawing = drawingHandler.GetActiveDrawing();

            if (activeDrawing != null)
            {
                drawingHandler.SaveActiveDrawing();
                drawingHandler.CloseActiveDrawing();
				
                Operation.RunMacro("sm_RepairModel.cs");
                Operation.RunMacro("sm_RepairLibrary.cs");
                Operation.RunMacro("sm_Numbering.cs");

                drawingHandler.SetActiveDrawing(activeDrawing, true);
            }
        }
    }
}
