using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArkadiumLibrary.Annotations;

namespace ArkadiumLibrary.ViewModels
{
    /// <summary>
    ///     Use to Prevent repetition of Implementing INotifyPropertyChanged Methods and OnPropertyChanged Calls
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            //only changes value if new value is different
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;
            //notifies property changed
            OnPropertyChanged(propertyName);
        }
    }
}