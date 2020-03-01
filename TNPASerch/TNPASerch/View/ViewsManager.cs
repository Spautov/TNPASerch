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
    }
}
