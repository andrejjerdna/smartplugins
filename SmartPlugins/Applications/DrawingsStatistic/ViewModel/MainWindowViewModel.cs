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

namespace DrawingsStatistic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public SmartModel SmartModel
        {
            get => Get(new SmartModel());
            set => Set(value);
        }

        public ObservableCollection<DrawingData> DrawingDatas
        {
            get => GetColl<DrawingData>();
            set => SetColl(value);
        }

        public TeklaDrawingsObserver TeklaDrawingsObserver
        {
            get => Get(new TeklaDrawingsObserver());
            set => Set(value);
        }

        public MainWindowViewModel()
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += GetData;
            timer.AutoReset = true;
            timer.Enabled = true;

            TeklaDrawingsObserver = new TeklaDrawingsObserver();
        }

        private void GetData(Object source, ElapsedEventArgs e)
        {
            foreach(var data in TeklaDrawingsObserver.DrawingDatas())
            {
                DrawingDatas.Add(data);
            }
        }
    }
}

