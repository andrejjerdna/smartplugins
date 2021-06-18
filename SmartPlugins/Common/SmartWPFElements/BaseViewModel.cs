using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SmartWPFElements
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, object> values = new Dictionary<string, object>();
        public TaskRunner Tasks;

        public BaseViewModel()
        {
            Tasks = new TaskRunner(this);
        }

        protected void SetColl<T>(ObservableCollection<T> val, [CallerMemberName] string propertyName = "")
        {
            Set(val, propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ObservableCollection<T> GetColl<T>([CallerMemberName] string propertyName = "")
        {
            if (values.ContainsKey(propertyName))
                return (ObservableCollection<T>)values[propertyName];
            return (ObservableCollection<T>)(values[propertyName] = new ObservableCollection<T>());
        }

        protected void SetStr(string value, [CallerMemberName] string propertyName = "")
        {
            Set(value, propertyName);
        }

        protected string GetStr(string defaultValue, [CallerMemberName] string propertyName = "")
        {
            return Get(defaultValue, propertyName);
        }

        protected bool GetBool(bool defaultValue, [CallerMemberName] string propertyName = "")
        {
            return Get(defaultValue, propertyName);
        }

        protected T Get<T>(T defaultValue, [CallerMemberName] string propertyName = "")
        {
            if (values.TryGetValue(propertyName, out object v))
                return (T)v;
            return defaultValue;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            values[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispatch(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action);
        }

        public void ShowMsg(string message)
        {
            MessageBox.Show(message, "");
        }
    }
}
