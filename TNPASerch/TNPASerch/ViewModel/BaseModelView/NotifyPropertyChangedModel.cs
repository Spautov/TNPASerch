using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TNPASerch.ViewModel
{
    public abstract class NotifyPropertyChangedModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
