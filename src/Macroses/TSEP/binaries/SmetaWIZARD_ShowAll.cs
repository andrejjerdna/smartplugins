using Tekla.Technology;

[assembly: Tekla.Technology.Scripting.Compiler.Reference(@"%XSDATADIR%\Environments\common\macros\modeling\WizardSoft.SmetaAddIn.TeklaExtension.dll")]

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            new WizardSoft.SmetaAddIn.TeklaExtension.TeklaViewsOperations().RedrawView();
        }
    }
}
