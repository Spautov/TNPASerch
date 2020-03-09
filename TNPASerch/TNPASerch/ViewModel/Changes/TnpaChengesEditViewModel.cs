using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class TnpaChengesEditViewModel : BaseViewModel
    {
        public ICommand AddChangeCommand { get; set; }
        public ICommand RemoveChangeCommand { get; set; }
        public ICommand EditChangeCommand { get; set; }
        public ICommand CancelChangeCommand { get; set; }

        private readonly Tnpa _tnpa;

        private ObservableCollection<Change> _changes;
        public ObservableCollection<Change> Changes
        {
            get { return _changes; }
            set
            {
                _changes = value;
                OnPropertyChanged();
            }
        }

        private Change _selectedChange;
        public Change SelectedChange
        {
            get { return _selectedChange; }
            set
            {
                _selectedChange = value;
                OnPropertyChanged();
            }
        }

        public TnpaChengesEditViewModel(TnpaChengesEditView window, Tnpa tnpa) //: base(window)
        {
            _tnpa = tnpa ?? throw new ArgumentNullException(nameof(tnpa));

            AddChangeCommand = new RelayCommand(AddChange);
            RemoveChangeCommand = new RelayCommand(RemoveChange);
            EditChangeCommand = new RelayCommand(EditChange);
            CancelChangeCommand = new RelayCommand(Close);

            GetChange();
        }

        private void EditChange()
        {
            if (SelectedChange != null)
            {
                ChangeView changeView = ViewsManager.ChangeEditView(_tnpa, SelectedChange.Number);
                changeView.ShowDialog();
                GetChange();
            }
        }

        private void RemoveChange()
        {
            if (SelectedChange != null)
            {
                _tnpa.Changes.Remove(SelectedChange);
                GetChange();
            }
        }

        private void AddChange()
        {
            ChangeView changeView = ViewsManager.ChangeView(_tnpa);
            changeView.ShowDialog();
            GetChange();
        }

        private void GetChange()
        {
            Changes = new ObservableCollection<Change>(_tnpa.Changes);
        }
    }
}
