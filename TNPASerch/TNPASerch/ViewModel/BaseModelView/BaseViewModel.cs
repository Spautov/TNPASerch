using System;
using System.Windows;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public abstract class BaseViewModel : NotifyPropertyChangedModel
    {
        protected readonly Window _window;

        public BaseViewModel(Window window)
        {
            _window = window ?? throw new ArgumentNullException("window");
        }

        protected bool YesNoCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel(messageView)
            {
                Message = message,
                Title = title,
                IsNOButtonVisible = Visibility.Visible
            };
            messageView.DataContext = messageViewModel;
            messageView.ShowDialog();

            return messageViewModel.ButtonResult;
        }

        protected bool YesCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel(messageView)
            {
                Message = message,
                Title = title,
                IsNOButtonVisible = Visibility.Collapsed
            };
            messageView.DataContext = messageViewModel;
            messageView.ShowDialog();

            return messageViewModel.ButtonResult;
        }

        protected bool YesMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel(messageView)
            {
                Message = message,
                Title = title,
                OkButtonTitle = "Ok",
                IsNOButtonVisible = Visibility.Collapsed,
                IsCancelButtonVisible = Visibility.Hidden
            };
            messageView.DataContext = messageViewModel;
            messageView.ShowDialog();

            return messageViewModel.ButtonResult;
        }

        protected void Close()
        {
            _window.Close();
        }
    }
}
