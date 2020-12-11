using SmartWPFElements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Tekla.Structures.Dialog;
using tsd = Tekla.Structures.Datatype;

namespace ConcreteFoundationPlugin
{
    public class PluginViewModel : INotifyPropertyChanged
    {
        public PluginViewModel()
        {
            _concreteDetail1 = new ConcreteDetail();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public ICommand MainPartParameters
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    OnPropertyChanged(nameof(ConcreteDetail1));
                    var parametersWindow = new ConcreteDetailsWindow(_concreteDetail1);
                    parametersWindow.ShowDialog();

                    _concreteDetail1 = parametersWindow.DataContext as ConcreteDetail;
                });
            }
        }
        
        private ConcreteDetail _concreteDetail1;

        public ConcreteDetail ConcreteDetail1
        {
            get { return _concreteDetail1; }
            set { _concreteDetail1 = value; OnPropertyChanged(nameof(ConcreteDetail1)); }
        }
        
        private string _name = string.Empty;

        [StructuresDialog(nameof(Name), typeof(tsd.String))]

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private tsd.Distance _class = new tsd.Distance();

        [StructuresDialog(nameof(Class), typeof(tsd.Double))]

        public tsd.Distance Class
        {
            get { return _class; }
            set { _class = value; OnPropertyChanged(nameof(Class)); }
        }

        private string _profile = string.Empty;

        [StructuresDialog(nameof(Profile), typeof(tsd.String))]
        public string Profile
        {
            get { return _profile; }
            set { _profile = value; OnPropertyChanged(nameof(Profile)); }
        }
    }
}