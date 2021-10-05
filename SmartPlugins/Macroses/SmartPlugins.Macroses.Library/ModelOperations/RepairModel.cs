using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;

namespace SmartPlugins.Macroses.Library.ModelOperations
{
    public class RepairModel
    {
        public void StarRepairModel()
        {
            TeklaStructures.Connect();

            var macroBuilder = new MacroBuilder();

            macroBuilder.Callback("acmd_check_database", "1", "main_frame");
            macroBuilder.Callback("acmd_check_database", "XS_LIB_CORRECT", "main_frame");
            macroBuilder.Callback("acmd_partnumbers_all", "full", "main_frame");
            macroBuilder.PushButton("xs_save_button", "xs_report");
            macroBuilder.PushButton("warning_ok", "full_numbering_ok");

            macroBuilder.Run();
        }
    }
}
