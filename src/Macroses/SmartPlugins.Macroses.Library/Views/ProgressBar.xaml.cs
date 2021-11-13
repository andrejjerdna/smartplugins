using SmartPlugins.Common.Abstractions;
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
using System.Windows.Threading;

namespace SmartPlugins.Macroses.Library.Views
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : Window, IProgressWindow
    {
        public ProgressBar(IProgressBarViewModel progressBarViewModel)
        {
            InitializeComponent();
            DataContext = progressBarViewModel;
        }
    }
}
