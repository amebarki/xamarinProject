using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectIncident.Core.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> _propertyValues;



        public BaseViewModel()
        {
            _propertyValues = new Dictionary<string, object>();
        }

        public virtual object GetProperty([CallerMemberName] string propertyName = null, object defaultValue = null)
        {
            if (_propertyValues.ContainsKey(propertyName)) return _propertyValues[propertyName];
            return defaultValue;
        }
        public virtual bool SetProperty<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            object current = GetProperty(propertyName);

            if ((current == null && newValue == null) ||
                (current != null && EqualityComparer<T>.Default.Equals((T)current, newValue)))
            {
                return false;
            }

            _propertyValues[propertyName] = newValue;
            OnPropertyChanged(propertyName);

            return true;
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
