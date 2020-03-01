using DAL;
using System.Windows;
using TNPASerch.ViewModel;

namespace TNPASerch.View
{
    public static class ViewsManager
    {
        public static ChangeView ChangeEditView(Window owner, Tnpa tnpa, int selectedChangeNumber )
        {
            ChangeView changeView = new ChangeView
            {
                Owner = owner
            };
            ChangeEditViewModel changeEditViewModel =
                new ChangeEditViewModel(changeView, tnpa, selectedChangeNumber);
            changeView.DataContext = changeEditViewModel;
            return changeView;
        } 
        
        public static ChangeView ChangeView(Window owner, Tnpa tnpa)
        {
            ChangeView changeView = new ChangeView
            {
                Owner = owner
            };
            ChangeViewModel changeViewModel = new ChangeViewModel(changeView, tnpa);
            changeView.DataContext = changeViewModel;
            return changeView;
        }

        public static TnpaChengesEditView TnpaChengesEditView(Window owner, Tnpa tnpa)
        {
            var view = new TnpaChengesEditView
            {
                Owner = owner
            };

            var ViewModel = new TnpaChengesEditViewModel(view, tnpa);
            view.DataContext = ViewModel;
            return view;
        }

        public static TnpaTypeEditView TnpaTypeEditView(Window owner)
        {
            TnpaTypeEditView tnpaTypeEditView = new TnpaTypeEditView
            {
                Owner = owner
            };
            tnpaTypeEditView.DataContext = new TnpaTypeEditViewModel(tnpaTypeEditView);
            return tnpaTypeEditView;
        }

        public static TNPAWindow EditTNPAView(Window owner, Tnpa tnpa)
        {
            TNPAWindow addWindow = new TNPAWindow
            {
                Owner = owner
            };
            addWindow.DataContext = new EditTNPAViewModel(addWindow, tnpa);

            return addWindow;        
        }

        public static TNPAWindow AddTNPAView(Window owner)
        {
            TNPAWindow addWindow = new TNPAWindow
            {
                Owner = owner
            };
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
            return addWindow;
        }

    }
}
