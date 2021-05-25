using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gun2Core.ViewModels.Base
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public virtual string Title
        {
            get { return _Title; }
            set {
                value = $"Gun2 v.{typeof(ViewModel).Assembly.GetName().Version} : {value.Trim()}";
                Set(ref _Title, value); 
            }
        }

        private string _Title;

    }
}
