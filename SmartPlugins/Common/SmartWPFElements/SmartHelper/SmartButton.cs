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
        private ISmartHelperApp _smartHelperApp;

        public string TextButton { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; set; }

        public SmartButton(ISmartHelperApp smartHelperApp)
        {
            _smartHelperApp = smartHelperApp;

            TextButton = "Empty button";
            Icon = "WifiSettings";
            Command = GetCommand();
        }

        private ICommand GetCommand()
        {
            return new DelegateCommand((obj) =>
            {
                Task.Run(() =>
                {
                    _smartHelperApp.Run();
                });
            });
        }
    }
}
