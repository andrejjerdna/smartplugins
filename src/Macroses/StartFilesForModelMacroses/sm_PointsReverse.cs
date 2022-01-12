[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\SmartPlugins\SmartPlugins.Macros.Library.dll")]
[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\SmartPlugins\SmartPlugins.Common.Abstractions.dll")]

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            new SmartPlugins.Macros.Library.MacroLauncher()
                .MacroRunOnce<SmartPlugins.Macros.Library.PointsReverseMacro>();
        }
    }
}