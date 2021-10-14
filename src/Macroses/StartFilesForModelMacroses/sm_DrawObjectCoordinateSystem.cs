using SmartPlugins.Common.Abstractions;

[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\SmartPlugins.Macroses.Library.dll")]

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            try
            {
                new SmartPlugins.Macroses.Library.DrawObjectCoordinateSystemMacro().Run();
            }
            catch
            {

            }
        }
    }
}