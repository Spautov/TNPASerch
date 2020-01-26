using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly ICollection<Change> _tnpaChanges;

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

        public TnpaChengesEditViewModel(TnpaChengesEditView window, ICollection<Change> tnpaChanges) : base(window)
        {
            _tnpaChanges = tnpaChanges ?? throw new ArgumentNullException(nameof(tnpaChanges));

            AddChangeCommand = new RelayCommand(AddChange);
            RemoveChangeCommand = new RelayCommand(RemoveChange);
            EditChangeCommand = new RelayCommand(EditChange);
            CancelChangeCommand = new RelayCommand(Close);

            GetChange();
        }

        private void EditChange()
        {
            throw new NotImplementedException();
        }

        private void RemoveChange()
        {
            throw new NotImplementedException();
        }

        private void AddChange()
        {
            throw new NotImplementedException();
        }

        private void GetChange()
        {
            Changes = new ObservableCollection<Change>(_tnpaChanges);
        }
    }
}
