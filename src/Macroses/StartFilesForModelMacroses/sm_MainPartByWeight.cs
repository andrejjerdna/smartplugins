[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\SmartPlugins\SmartPlugins.Macroses.Library.dll")]
[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\SmartPlugins\SmartPlugins.Common.Abstractions.dll")]

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            new SmartPlugins.Macroses.Library.MacroLauncher()
                .MacroRunOnce<SmartPlugins.Macroses.Library.MainPartByWeightMacro>();
        }
    }
}