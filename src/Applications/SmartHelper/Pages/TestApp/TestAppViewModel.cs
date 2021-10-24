using SmartPlugins.Common.TeklaStructures;
using SmartPlugins.Common.SmartWPFElements;
using SmartPlugins.Common.SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartPlugins.Applications.SmartHelper.Pages.TestApp
{
    public class TestAppViewModel : BaseViewModel
    {
        public ICommand RunApp
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var app = new TestAppRunner();
                    app.Run();
                });
            }
        }
    }
}
