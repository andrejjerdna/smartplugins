using AxCoDesign.Applications.AutoConnect.Model;
using AxCoDesign.Applications.Library;
using AxCoDesign.ML.Library.AutoConnect;
using AxCoDesign.Plugins.Model.WPFPluginsBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseViewModel = AxCoDesign.Applications.Library.WindowsHelpers.BaseViewModel;

namespace AxCoDesign.Applications.AutoConnect.ViewModel
{
    public interface IProgressBarStatus
    {
        int ProgressBarMaxValue { get; set; }
        int ProgressBarValue { get; set; }
        string ProgressBarStatus { get; set; }
    }

    public class MainWindowViewModel : BaseViewModel, IProgressBarStatus
    {
        private IMLResultsViewer _resultViewer;
        private IConnectTrainer _connectTrainer;

        public TeklaModel TeklaModel
        {
            get => Get(new TeklaModel());
            set => Set(value);
        }

        public ObservableCollection<IModelData> TeklaModelDatas
        {
            get => GetColl<IModelData>();
            set => SetColl(value);
        }

        public ObservableCollection<TeklaPluginRules> DataRules
        {
            get => GetColl<TeklaPluginRules>();
            set => SetColl(value);
        }

        public IEnumerable<string> ListSettings
        {
            get => Get(new List<string>());
            set => Set(value);
        }

        public string UserSettings
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public string FilterName
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public string FilterConnectName
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public int NumberOfClusters
        {
            get => Get(2);
            set => Set(value);
        }

        public double Delta
        {
            get => Get(500.0);
            set => Set(value);
        }

        public IEnumerable<string> Filters
        {
            get => Get(new List<string>());
            set => Set(value);
        }

        public bool DataStatus
        {
            get => Get(false);
            set => Set(value);
        }

        public bool TrainingStatus
        {
            get => Get(false);
            set => Set(value);
        }

        public bool GetObjectsWithCurrentView
        {
            get => Get(false);
            set => Set(value);
        }

        public int SelectDataIndex
        {
            get => Get(-1);
            set => Set(value);
        }

        public int ProgressBarMaxValue
        {
            get => Get(1);
            set => Set(value);
        }
        public int ProgressBarValue
        {
            get => Get(0);
            set => Set(value);
        }

        public string ProgressBarStatus
        {
            get => Get(string.Empty);
            set => Set(value);
        }

        public ObservableCollection<string> ListSaveFiles
        {
            get => GetColl<string>();
            set => SetColl(value);
        }

        public bool TrainStatus
        {
            get => Get(false);
            set => Set(value);
        }

        private TeklaConnectionsCreator _teklaConnectionsCreator;

        public MainWindowViewModel()
        {
            _resultViewer = new MLResultsViewer(TeklaModel);

            _resultViewer.Progress.OnProgressChanged((e) =>
            {
                ProgressBarValue = e.MaxValue == 0 ? 0 : e.Value * 100 / e.MaxValue;
                ProgressBarMaxValue = 100;
                ProgressBarStatus = e.Message;
            });

            _connectTrainer = new ConnectTrainer(TeklaModel, TeklaModelDatas, DataRules);
        }

        /// <summary>
        /// Получение элементов из модели и дальнейшее обучение.
        /// </summary>
        public ICommand RunAppDataFromTeklaModel
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (TeklaModel.ConnectStatus == false)
                        return;

                    DataRules.Clear();
                    TrainStatus = false;
                    DataStatus = false;

                    TeklaModelDatas.Clear();

                    var datas = new TeklaModelDatas(TeklaModel, FilterName, Delta/2, TeklaModelDatas)
                    {
                        FilterConnectName = FilterConnectName,
                        GetObjectsWithCurrentView = GetObjectsWithCurrentView
                    };

                    var mlTrainer = new MLTrainer(TeklaModelDatas, NumberOfClusters, TeklaModel.PathAttributeModel);

                    datas.Progress.OnProgressChanged((e) =>
                    {
                        ProgressBarValue = e.MaxValue == 0 ? 0 : e.Value * 100 / e.MaxValue;
                        ProgressBarMaxValue = 100;
                        ProgressBarStatus = e.Message;
                    });

                    Tasks.Run(() =>
                    {
                        var result = datas.GetTeklaModelDatas();

                        DataStatus = result.Result;

                        var trainResult = mlTrainer.MLTraining();
                        TrainStatus = trainResult;

                        if (TrainStatus)
                            ProgressBarStatus = MLMessages.ModelTrainingDone;
                        else
                            ProgressBarStatus = MLMessages.ModelTrainingError;
                    });
                });
            }
        }

        /// <summary>
        /// Получение базы данных из ранее полученных данных и дальнейшее обучение.
        /// </summary>
        public ICommand RunAppDataFromFile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (TeklaModel.ConnectStatus == false)
                        return;

                    DataRules.Clear();
                    TrainStatus = false;

                    var mlTrainer = new MLTrainer(TeklaModelDatas, NumberOfClusters, TeklaModel.PathAttributeModel);

                    var trainResult = false;

                    Tasks.Run(() =>
                    {
                        trainResult = mlTrainer.MLTraining();
                        TrainStatus = trainResult;

                        if (TrainStatus)
                            ProgressBarStatus = MLMessages.ModelTrainingDone;
                        else
                            ProgressBarStatus = MLMessages.ModelTrainingError;

                    });
                });
            }
        }

        /// <summary>
        /// Обозначить в модели полученные типы узлов.
        /// </summary>
        public ICommand RunAppDrawConnectTypesInTeklaModel
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Task.Run(() => 
                    { 
                        _resultViewer.DrawConnectTypesInTeklaModel(NumberOfClusters, TeklaModelDatas);
                    });
                });
            }
        }

        /// <summary>
        /// Обучение модели расстановке узлов
        /// </summary>
        public ICommand RunAppTeklaModelTraining
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        TrainingStatus = false;
                        DataRules.Clear();
                        _connectTrainer.TeklaModelTraining();
                        ProgressBarStatus = MLMessages.ConnectTrainDone;
                        TrainingStatus = true;
                    }
                    catch
                    {
                        ProgressBarStatus = MLMessages.ConnectTrainError;
                    }
                });
            }
        }

        /// <summary>
        /// Вставка узлов в модель.
        /// </summary>
        public ICommand RunAppLearningComponentsInsert
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _teklaConnectionsCreator = new TeklaConnectionsCreator(TeklaModel, TeklaModelDatas, DataRules, Delta/2, NumberOfClusters);

                    _teklaConnectionsCreator.Progress.OnProgressChanged((e) =>
                    {
                        ProgressBarValue = e.MaxValue == 0 ? 0 : e.Value * 100 / e.MaxValue;
                        ProgressBarMaxValue = 100;
                        ProgressBarStatus = e.Message;
                    });

                    Tasks.Run(() =>
                    {
                        var result = _teklaConnectionsCreator.InsertConnections();

                        if (result.Result)
                            ProgressBarStatus = MLMessages.InsertConnecrionsDone;
                        else
                            ProgressBarStatus = MLMessages.ConnectTrainError;

                    });
                });
            }
        }

        /// <summary>
        /// Удаление вставленных узлов.
        /// </summary>
        public ICommand RunAppLearningComponentsDelete
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    _teklaConnectionsCreator?.Progress.OnProgressChanged((e) =>
                    {
                        ProgressBarValue = e.MaxValue == 0 ? 0 : e.Value * 100 / e.MaxValue;
                        ProgressBarMaxValue = 100;
                        ProgressBarStatus = e.Message;
                    });

                    Tasks.Run(() =>
                    {
                        _teklaConnectionsCreator?.DeleteConnections();
                    });
                });
            }
        }
    }
}
