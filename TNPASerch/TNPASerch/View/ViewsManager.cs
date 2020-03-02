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
                new ChangeEditViewModel(changeView, tnpa, selectedChangeNumber);
            changeView.DataContext = changeEditViewModel;
            return changeView;
        } 
        
        public static ChangeView ChangeView(Tnpa tnpa)
        {
            ChangeView changeView = new ChangeView
            {
                Owner = App.Current.MainWindow
            };
            ChangeViewModel changeViewModel = new ChangeViewModel(changeView, tnpa);
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
            addWindow.DataContext = new EditTNPAViewModel(addWindow, tnpa);

            return addWindow;        
        }

        public static TNPAWindow AddTNPAView()
        {
            TNPAWindow addWindow = new TNPAWindow
            {
                Owner = App.Current.MainWindow
            };
            
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
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

    }
}
