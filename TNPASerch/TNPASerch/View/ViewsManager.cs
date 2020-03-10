using DAL;
using System.Windows;
using TNPASerch.ViewModel;

namespace TNPASerch.View
{
    public static class ViewsManager
    {
        public static ChangeView ChangeEditView(Tnpa tnpa, int selectedChangeNumber )
        {
            ChangeView changeView = new ChangeView
            {
                Owner = App.Current.MainWindow
            };
            ChangeEditViewModel changeEditViewModel =
                new ChangeEditViewModel(tnpa, selectedChangeNumber);
            changeView.DataContext = changeEditViewModel;
            return changeView;
        } 
        
        public static ChangeView ChangeView(Tnpa tnpa)
        {
            ChangeView changeView = new ChangeView
            {
                Owner = App.Current.MainWindow
            };
            ChangeViewModel changeViewModel = new ChangeViewModel(tnpa);
            changeView.DataContext = changeViewModel;
            return changeView;
        }

        public static TnpaChengesEditView TnpaChengesEditView(Tnpa tnpa)
        {
            var view = new TnpaChengesEditView
            {
                Owner = App.Current.MainWindow
            };

            var ViewModel = new TnpaChengesEditViewModel(view, tnpa);
            view.DataContext = ViewModel;
            return view;
        }

        public static TnpaTypeEditView TnpaTypeEditView()
        {
            TnpaTypeEditView tnpaTypeEditView = new TnpaTypeEditView
            {
                Owner = App.Current.MainWindow
            };
            tnpaTypeEditView.DataContext = new TnpaTypeEditViewModel(tnpaTypeEditView);
            return tnpaTypeEditView;
        }

        public static TNPAWindow EditTNPAView(Tnpa tnpa)
        {
            TNPAWindow addWindow = new TNPAWindow
            {
                Owner = App.Current.MainWindow
            };
            addWindow.DataContext = new EditTNPAViewModel(tnpa);

            return addWindow;        
        }

        public static TNPAWindow AddTNPAView()
        {
            TNPAWindow addWindow = new TNPAWindow
            {
                Owner = App.Current.MainWindow
            };
            
            addWindow.DataContext = new AddTNPAViewModel();
            return addWindow;
        }

        public static EditFilesView EditFilesView()
        {
            EditFilesView editFilesView = new EditFilesView
            { 
                Owner = App.Current.MainWindow
            };
            editFilesView.DataContext = new EditFilesViewModel();
            return editFilesView;
        }

        public static MessageView YesMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel()
            {
                Message = message,
                Title = title,
                OkButtonTitle = "Ok",
                IsNOButtonVisible = Visibility.Collapsed,
                IsCancelButtonVisible = Visibility.Hidden
            };
            messageView.DataContext = messageViewModel;
            return messageView;
        }

        public static MessageView YesCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel()
            {
                Message = message,
                Title = title,
                IsNOButtonVisible = Visibility.Collapsed
            };
            messageView.DataContext = messageViewModel;
            return messageView;
        }

        public static MessageView YesNoCancelMessage(string message = "", string title = "")
        {
            MessageView messageView = new MessageView
            {
                Owner = App.Current.MainWindow
            };
            MessageViewModel messageViewModel = new MessageViewModel()
            {
                Message = message,
                Title = title,
                IsNOButtonVisible = Visibility.Visible
            };
            messageView.DataContext = messageViewModel;
            return messageView;
        }
    }
}
