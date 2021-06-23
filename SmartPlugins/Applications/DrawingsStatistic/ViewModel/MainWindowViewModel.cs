using DrawingsStatistic.Model;
using SmartTeklaModel;
using SmartWPFElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            BindingOperations.EnableCollectionSynchronization(_drawingDatas, _lock);

            TeklaDrawingsObserver = new TeklaDrawingsObserver(_drawingDatas);

            Tasks.Run(() =>
            {
                TeklaDrawingsObserver.StartObserver();
            });
        }
    }
}

