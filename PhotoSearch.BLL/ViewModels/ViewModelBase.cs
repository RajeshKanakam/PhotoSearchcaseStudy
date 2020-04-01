using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhotoSearch.BLL.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null && !string.IsNullOrWhiteSpace(property))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
