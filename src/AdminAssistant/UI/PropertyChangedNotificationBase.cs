using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdminAssistant.UI
{
    public abstract class PropertyChangedNotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null!;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingFiled, T value, [CallerMemberName] string propertyName = "", Action? validationCallBack = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value))
                return;

            backingFiled = value;
            validationCallBack?.Invoke();
            this.OnPropertyChanged(propertyName);
        }
    }
}
