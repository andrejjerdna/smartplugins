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
using System.Windows.Shapes;

namespace SmartPlugins.Common.SmartWPFElements
{
    /// <summary>
    /// Interaction logic for ConcreteDetailsWindow.xaml
    /// </summary>
    public partial class ConcreteDetailsWindow : Window
    {
        public ConcreteDetailsWindow(ConcreteDetail concreteDetailViewModel)
        {
            InitializeComponent();
            DataContext = concreteDetailViewModel;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
