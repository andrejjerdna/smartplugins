using SmartHelper.Pages.TestApp;
using SmartWPFElements;
using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartHelper.ViewModel
{
    public class ConcreteWindowViewModel : SmartHelperViewModel
    {
        public ConcreteWindowViewModel()
        {
            var steelApps = GetSteelApps();
            SmartButtons = new ObservableCollection<SmartButton>(steelApps);
        }

        private IEnumerable<SmartButton> GetSteelApps()
        {
            var steelApps = new List<SmartButton>
            {
                //new SmartButton()
                //{
                //    TextButton = "Test",
                //    Icon = "EmoticonCool",
                //    SmartHelperApp = new TestAppRunner(),
                //    UI = new TestApp(),
                //},

                //new SmartButton()
                //{
                //    TextButton = "Test2",
                //    Icon = "EmoticonCool",
                //    SmartHelperApp = new TestAppRunner(),
                //    UI = new TestApp(),
                //}
            };

            return steelApps;
        }
    }
}
