using SmartPlugins.Applications.SmartHelper.Pages;
using SmartPlugins.Applications.SmartHelper.Pages.TestApp;
using SmartPlugins.Common.SmartWPFElements;
using SmartPlugins.Common.SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartPlugins.Applications.SmartHelper.ViewModel
{
    public class SteelWindowViewModel : SmartHelperViewModel
    {
        ITestAppRunner _testAppRunner;

        public SteelWindowViewModel(ITestAppRunner testAppRunner)
        {
            var steelApps = GetSteelApps();
            SmartButtons = new ObservableCollection<SmartButton>(steelApps);

            _testAppRunner = testAppRunner;
        }

        private IEnumerable<SmartButton> GetSteelApps()
        {
            var steelApps = new List<SmartButton>
            {
                new SmartButton(_testAppRunner)
            };

            return steelApps;
        }
    }
}
