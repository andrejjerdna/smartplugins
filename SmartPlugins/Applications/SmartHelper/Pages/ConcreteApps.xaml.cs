using SmartHelper.ViewModel;
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

namespace SmartHelper.Pages
{
    /// <summary>
    /// Interaction logic for ConcreteApps.xaml
    /// </summary>
    public partial class ConcreteApps : UserControl
    {
        private ConcreteWindowViewModel _concreteWindowViewModel;

        public ConcreteApps(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();

            _concreteWindowViewModel = new ConcreteWindowViewModel(mainWindowViewModel);
            DataContext = _concreteWindowViewModel;
        }
    }
}
