using SmartPlugins.Applications.SmartHelper.ViewModel;
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

namespace SmartPlugins.Applications.SmartHelper.Pages
{
    /// <summary>
    /// Interaction logic for SteelApps.xaml
    /// </summary>
    public partial class SteelApps : UserControl
    {
        private SteelWindowViewModel _steelWindowViewModel;

        public SteelApps()
        {
            InitializeComponent();

            //_steelWindowViewModel = new SteelWindowViewModel();
            //DataContext = _steelWindowViewModel;
        }
    }
}
