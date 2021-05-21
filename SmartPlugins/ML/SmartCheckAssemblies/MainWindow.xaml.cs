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
using Tekla.Structures.Model;

namespace SmartCheckAssemblies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SmartCheckAssembliesViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new SmartCheckAssembliesViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RunApp();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _viewModel.RunBug();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _viewModel.RunTrain();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _viewModel.RunPredict();
        }
    }
}
