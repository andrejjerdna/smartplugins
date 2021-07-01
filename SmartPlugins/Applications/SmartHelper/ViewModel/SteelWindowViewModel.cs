using SmartHelper.Pages;
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
    public class SteelWindowViewModel : BaseViewModel
    {
        private Frame _frameMainWindow;

        public ObservableCollection<SmartButton> SmartButtons
        {
            get => GetColl<SmartButton>();
            set => SetColl(value);
        }

        public SteelWindowViewModel(Frame frameMainWindow)
        {
            _frameMainWindow = frameMainWindow;

            var steelApps = GetSteelApps();
            SmartButtons = new ObservableCollection<SmartButton>(steelApps);
        }

        private IEnumerable<SmartButton> GetSteelApps()
        {
            var steelApps = new List<SmartButton>
            {
                new SmartButton()
                {
                    TextButton = "Test",
                    Icon = "EmoticonCool",
                    FrameMainWindow = _frameMainWindow,
                    SmartHelperApp = new TestApp(),
                    PageApp = new TestPage()
                },

                new SmartButton()
                {
                    TextButton = "Test2",
                    Icon = "EmoticonCool",
                    FrameMainWindow = _frameMainWindow,
                    SmartHelperApp = new TestApp(),
                    PageApp = new TestPage()
                }
            };

            return steelApps;
        }
    }
}
