using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class MessageViewModel: NotifyPropertyChangedModel
    {
        private readonly MessageView _window;
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public bool ButtonResult { get; set; }
        public Visibility IsNOButtonVisible { get; set; }
        public Visibility IsCancelButtonVisible { get; set; }
        public string OkButtonTitle { get; set; }
        public string CancelButtonTitle { get; set; }
        public string NoButtonTitle { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Сообщение для отображения в окне
        /// </summary>
        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public MessageViewModel(MessageView window)
        {
            _window = window ?? throw new ArgumentNullException("window");
            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            IsNOButtonVisible = Visibility.Visible ;
            IsCancelButtonVisible = Visibility.Visible ;
            OkButtonTitle = "Да";
            NoButtonTitle = "Нет";
            CancelButtonTitle = "Отмена";
        }

        private void Cancel()
        {
            ButtonResult = false;
            _window.Close();
        }

        private void Ok()
        {
            ButtonResult = true;
            _window.Close();
        }
    }
}
