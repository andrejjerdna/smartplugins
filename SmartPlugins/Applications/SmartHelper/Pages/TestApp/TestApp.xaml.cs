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

namespace SmartHelper.Pages.TestApp
{
    /// <summary>
    /// Interaction logic for TestApp.xaml
    /// </summary>
    public partial class TestApp : UserControl
    {
        private TestAppViewModel _testApp;

        public TestApp()
        {
            InitializeComponent();
            _testApp = new TestAppViewModel();

            DataContext = _testApp;
        }
    }
}
