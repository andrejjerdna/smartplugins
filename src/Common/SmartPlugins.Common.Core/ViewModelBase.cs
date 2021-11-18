using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartPlugins.Common.Core
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
    }
}
