using System.ComponentModel;

namespace Ugly.Yaua.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaiseProperty(string propertyName)
        {
            var prop = PropertyChanged;

            if (prop != null)
            {
                prop(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
