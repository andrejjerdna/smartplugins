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
        public ISmartHelperApp SmartHelperApp { get; set; }
        public string TextButton { get; set; }
        public string Icon { get; set; }
        public ICommand Command { get; }
        public Frame FrameMainWindow { get; set; }
        public Page PageApp { get; set; }

        public SmartButton()
        {
            SmartHelperApp = null;
            TextButton = "Empty button";
            Icon = "WifiSettings";
            Command = GetCommand();
        }

        private ICommand GetCommand()
        {
            return new DelegateCommand((obj) =>
            {
                if (SmartHelperApp != null || PageApp != null)
                    FrameMainWindow.Navigate(PageApp);
            });
        }
    }
}
