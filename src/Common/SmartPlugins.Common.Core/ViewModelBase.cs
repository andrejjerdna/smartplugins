using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartPlugins.Common.Core
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected void SetColl<T>(ObservableCollection<T> val, [CallerMemberName] string propertyName = "")
        {
            Set(val, propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected ObservableCollection<T> GetColl<T>([CallerMemberName] string propertyName = "")
        {
            if (_values.ContainsKey(propertyName))
                return (ObservableCollection<T>)_values[propertyName];
            return (ObservableCollection<T>)(_values[propertyName] = new ObservableCollection<T>());
        }

        protected void SetString(string value, [CallerMemberName] string propertyName = "")
        {
            Set(value, propertyName);
        }

        protected string GetString(string defaultValue, [CallerMemberName] string propertyName = "")
        {
            return Get(defaultValue, propertyName);
        }

        protected bool GetBool(bool defaultValue, [CallerMemberName] string propertyName = "")
        {
            return Get(defaultValue, propertyName);
        }

        protected T Get<T>(T defaultValue, [CallerMemberName] string propertyName = "")
        {
            if (_values.TryGetValue(propertyName, out object v))
                return (T)v;
            return defaultValue;
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            _values[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
