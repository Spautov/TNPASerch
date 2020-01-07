using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTextViewModel : NotifyPropertyChangedModel
    {
        private readonly AddTextView _window;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddTextViewModel(AddTextView window)
        {
            _window = window ?? throw new ArgumentNullException("window");
            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel()
        {
            _window.DialogResult = false;
            _window.Close();
        }

        private void Ok()
        {
            _window.DialogResult = true;
            _window.Close();
        }

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
