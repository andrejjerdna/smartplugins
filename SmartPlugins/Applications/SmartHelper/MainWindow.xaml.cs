using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SmartHelper.ViewModel;

namespace SmartHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            await this.ShowMessageAsync("This is the title", "Some message");
        }

        private void OnSteelClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.SteelPage1());
        }

        private void OnConcreteClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.ConcretePage1());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
