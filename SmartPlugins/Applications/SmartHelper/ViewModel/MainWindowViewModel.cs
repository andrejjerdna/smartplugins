using SmartHelper.Pages;
using SmartHelper.Pages.TestApp;
using SmartTeklaModel;
using SmartWPFElements;
using SmartWPFElements.SmartHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SmartHelper.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Здесь создается объект, который в себе содержит модель теклы, а также различные расширения.
        /// В данный момент это в основном пути к папкам модели. 
        /// Постепенно этот класс будет расширяться.
        /// </summary>
        public SmartModel SmartModel
        {
            get => Get(new SmartModel());
            set => Set(value);
        }

        public UserControl InnerContent
        {
            get => Get(new SteelApps());
            set => Set(value);
        }

        public MainWindowViewModel()
        {
            InnerContent = new WelcomApps();
        }
    }
}
