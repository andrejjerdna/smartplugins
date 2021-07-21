using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartWPFElements
{
    public class SmartButton
    {
        public ISmartHelperApp _smartHelperApp { get; set; }
        public string TextButton { get; }
        public string Icon { get; set; }
        public ICommand Command { get; }
        public UserControl UI { get; set; }
        public object Presenter { get; set; }

        public SmartButton(ISmartHelperApp smartHelperApp)
        {
            _smartHelperApp = smartHelperApp;
            TextButton = _smartHelperApp.Name;

            Icon = "WifiSettings";
/*            Command = _smartHelperApp.Run()*/;
        }
    }
}
