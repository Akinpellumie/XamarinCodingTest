using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Steer73.FormsApp.Framework
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
