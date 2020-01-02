using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTextViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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


        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
