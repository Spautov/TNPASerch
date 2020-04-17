using System.Windows;

namespace TNPASerch.ViewModel
{
    public class MessageViewModel: NotifyPropertyChangedModel
    {
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
        public MessageViewModel()
        {
            IsNOButtonVisible = Visibility.Visible ;
            IsCancelButtonVisible = Visibility.Visible ;
            OkButtonTitle = "Да";
            NoButtonTitle = "Нет";
            CancelButtonTitle = "Отмена";
        }
    }
}
