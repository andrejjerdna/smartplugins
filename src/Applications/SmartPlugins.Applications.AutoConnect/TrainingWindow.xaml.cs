using AxCoDesign.Applications.AutoConnect.Model;
using AxCoDesign.Applications.AutoConnect.ViewModel;
using AxCoDesign.Applications.Library.Extensions;
using AxCoDesign.ML.Library.AutoConnect;
using System;
using System.Collections;
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
using Tekla.Structures.Dialog;
using Tekla.Structures.Model;
using Tekla.Structures.ModelInternal;
using InputType = AxCoDesign.ML.Library.AutoConnect.InputType;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;


namespace AxCoDesign.Applications.AutoConnect
{
    /// <summary>
    /// Interaction logic for TrainingWindow.xaml
    /// </summary>
    public partial class TrainingWindow : ApplicationWindowBase
    {
        private Events _modelEvents;
        private TrainingWindowViewModel _trainingWindowViewModel;

        public TrainingWindow()
        {
            InitializeComponent();
            _trainingWindowViewModel = new TrainingWindowViewModel();
            DataContext = _trainingWindowViewModel;

            _modelEvents = new Events();
            _modelEvents.SelectionChange += ModelEvents_SelectionChanged;
            _modelEvents.Register();
        }

        private void ModelEvents_SelectionChanged()
        {
            var objects = new ModelObjectSelector().GetSelectedObjects();

            foreach (var obj in objects)
            {
                if (obj is Connection connection)
                {
                    _trainingWindowViewModel.NameComponent = connection.Name;
                    _trainingWindowViewModel.NumberComponent = connection.Number;
                    _trainingWindowViewModel.TypePlugin = TypePluginEnum.Connection;
                }

                if (obj is Component component)
                {
                    _trainingWindowViewModel.NameComponent = component.Name;
                    _trainingWindowViewModel.NumberComponent = component.Number;
                    _trainingWindowViewModel.TypePlugin = TypePluginEnum.Component;
                }

                break;
            }
        }

        public TeklaPluginRules GetUserInput(IModelData appData)
        {
            var objects = new ModelObjectSelector().GetSelectedObjects();

            var pluginName = "";
            var pluginNumber = -1;

            var pluginType = PluginRulesEnum.MAINPART;

            var inputTypeMain = InputType.ONE_BEAM;
            var inputTypeSecondary = InputType.ONE_BEAM;

            var mainParts = new List<Part>();
            var secondaryParts = new List<Part>();

            foreach (var obj in objects)
            {
                if (obj is Connection connection)
                {
                    pluginName = connection.Name;
                    pluginNumber = connection.Number;

                    mainParts.Add(connection.GetPrimaryObject() as Part);
                    secondaryParts.AddRange(connection.GetSecondaryObjects().OfType<Part>());

                    _trainingWindowViewModel.TypePlugin = TypePluginEnum.Connection;

                    connection.Delete();
                }

                if (obj is Component component)
                {
                    pluginName = component.Name;
                    pluginNumber = component.Number;

                    var inputs = component.GetComponentInput().GetEnumerator().ToIEnumerable<InputItem>();

                    var count = 0;

                    foreach(var input in inputs)
                    {
                        if (count == 0)
                        {
                            if (input.GetData() is Part part)
                                mainParts.Add(part);

                            if (input.GetData() is ArrayList parts)
                            {
                                inputTypeMain = InputType.COLLECTION;
                                mainParts.AddRange(parts.OfType<Part>());
                            }
                        }

                        if (count == 1)
                        {
                            if (input.GetData() is Part part)
                                secondaryParts.Add(part);

                            if (input.GetData() is ArrayList parts)
                            {
                                inputTypeSecondary = InputType.COLLECTION;
                                secondaryParts.AddRange(parts.OfType<Part>());
                            }
                        }

                        count++;
                    }

                    _trainingWindowViewModel.TypePlugin = TypePluginEnum.Component;

                    component.Delete();
                }

                break;
            }

            var s2 = secondaryParts.Where(mo => mo.Identifier.GUID == appData.GUID);

            if (s2.Count() > 0)
                pluginType = PluginRulesEnum.SECONDARYPART;

            if (_trainingWindowViewModel.DublicatePluginString.Contains("Нет"))
                _trainingWindowViewModel.DublicatePlugin = false;
            else
                _trainingWindowViewModel.DublicatePlugin = true;

            return new TeklaPluginRules
            {
                Prediction = appData.Predict,
                AppData = appData,
                NameComponent = _trainingWindowViewModel.NameComponent,
                NumberComponent = _trainingWindowViewModel.NumberComponent,
                UserSetting = _trainingWindowViewModel.UserSettings,
                MainParts = mainParts,
                SecondaryParts = secondaryParts,
                PluginRulesType = pluginType,
                DublicatePlugin = _trainingWindowViewModel.DublicatePlugin,
                TypePlugin = _trainingWindowViewModel.TypePlugin,
                InputTypeMain = inputTypeMain,
                InputTypeSecondary = inputTypeSecondary
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
