using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\modeling\SmartMacroses.dll")]

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                SmartMacroses.CopyParametersRebarSets.Run();
            }
            catch
            {

            }
        }
    }
}