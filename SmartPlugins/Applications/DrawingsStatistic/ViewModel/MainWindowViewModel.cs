using DrawingsStatistic.Model;
using Newtonsoft.Json;
using SmartTeklaModel;
using SmartWPFElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Data;
using System.Windows.Threading;

namespace DrawingsStatistic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas.txt");
        private string _appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

        public SmartModel SmartModel
        {
            get => Get(new SmartModel());
            set => Set(value);
        }

        private ObservableCollection<DrawingData> _drawingDatas
        {
            get => GetColl<DrawingData>();
            set => SetColl(value);
        }

        private object _lock = new object();

        public ObservableCollection<DrawingData> DrawingDatas { get { return _drawingDatas; } }

        public TeklaDrawingsObserver TeklaDrawingsObserver
        {
            get => Get(new TeklaDrawingsObserver(_drawingDatas));
            set => Set(value);
        }

        public MainWindowViewModel()
        {
            Loaded();

            BindingOperations.EnableCollectionSynchronization(_drawingDatas, _lock);

            TeklaDrawingsObserver = new TeklaDrawingsObserver(_drawingDatas);
        }

        private void Loaded()
        {
            try
            {
                var datasDB = "";

                if (File.Exists(_filePath))
                {
                    datasDB = File.ReadAllText(_filePath);
                }

                if (datasDB == "")
                    return;

                var datas = JsonConvert.DeserializeObject<ObservableCollection<DrawingData>>(datasDB);

                foreach (var data in datas)
                    _drawingDatas.Add(data);
            }
            catch
            {

            }
        }

        public void Closing()
        {
            var datas = JsonConvert.SerializeObject(_drawingDatas);

            File.WriteAllText(_filePath, datas, Encoding.UTF8);
        }
    }
}

