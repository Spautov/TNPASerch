using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTextViewModel : BaseViewModel
    {
        private string _textValue;
        public string TextValue
        {
            get { return _textValue; }
            set
            {
                _textValue = value;
                OnPropertyChanged();
            }
        }
    }
}
