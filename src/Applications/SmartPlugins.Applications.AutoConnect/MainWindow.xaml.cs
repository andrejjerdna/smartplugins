using AxCoDesign.Applications.AutoConnect.Model;
using AxCoDesign.Applications.AutoConnect.ViewModel;
using AxCoDesign.Applications.Library.Model;
using AxCoDesign.Applications.Library.Serialization;
using AxCoDesign.Plugins.Model;
using AxCoDesign.Plugins.Model.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace AxCoDesign.Applications.AutoConnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ApplicationWindowBase
    {
        MainWindowViewModel _viewModel;

        private readonly string _pahtSaves = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

        private readonly string _fileExtension = ".json";

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            this.DataContext = _viewModel;

            if (!Directory.Exists(_pahtSaves))
                Directory.CreateDirectory(_pahtSaves);
        }

        private void Filters_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                _viewModel.Filters = GetAttributesFiles.GetFilter();
            }
            catch
            {

            }
        }

        private void EditDatas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mlTrainer = new ConnectTrainer(_viewModel.TeklaModel, _viewModel.TeklaModelDatas, _viewModel.DataRules);

                var sel = (uint)_viewModel.SelectDataIndex + 1;

                 var data = mlTrainer.TeklaModelTraining(sel);

                _viewModel.DataRules[_viewModel.SelectDataIndex] = data;
            }

            catch
            {
                _viewModel.ProgressBarStatus = MLMessages.EditError;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonSerialization.GetJSONString(_viewModel);

            var fileName = SaveNames.Text;

            if (!string.IsNullOrEmpty(fileName))
            {
                var path = System.IO.Path.Combine(_pahtSaves, fileName + _fileExtension);
                JsonSerialization.WriteToFile(json,path);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fileName = SaveNames.SelectedValue as string;


        }

        private void SaveNames_MouseEnter(object sender, MouseEventArgs e)
        {
            _viewModel.ListSaveFiles = new ObservableCollection<string>(Directories.FileNames(_pahtSaves, new List<string> { _fileExtension }));
        }
    }
}
