using GalaSoft.MvvmLight.Command;
using Repository;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TNPASerch.Model;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IRepository _repository;
        public ICommand ShowAddTNPAWindowCommand { get; set; }
        public ICommand ShowTNPATypeEditWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public MainWindowViewModel(MainWindow window) : base(window)
        {
            _repository = SQLiteRepository.GetRepository();
            GetTnpaTypsAsync();
            GetTnpaAsync();
            ShowAddTNPAWindowCommand = new RelayCommand(ShowAddTNPAWindow);
            ShowTNPATypeEditWindowCommand = new RelayCommand(ShowTNPATypeEditWindow);
            SearchCommand = new RelayCommand(SearchAsync);
            CloseCommand = new RelayCommand(Close);
        }

        private void ShowTNPATypeEditWindow()
        {
            TnpaTypeEditView tnpaTypeEditView = new TnpaTypeEditView
            {
                Owner = _window
            };
            tnpaTypeEditView.DataContext = new TnpaTypeEditViewModel(tnpaTypeEditView);
            tnpaTypeEditView.ShowDialog();

            GetTnpaTypsAsync();
            GetTnpaAsync();
        }

        private async void SearchAsync()
        {
            await Task.Run(() => { Search(); });
        }

        private void Search()
        {
            // to do
            MessageBox.Show(NumberTnpa);
            NumberTnpa = "";
        }

        public string _numberTnpa;
        public string NumberTnpa
        {
            get { return _numberTnpa; }
            set
            {
                _numberTnpa = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TnpaTypeView> _tnpaTypes;
        public ObservableCollection<TnpaTypeView> TnpaTypes
        {
            get { return _tnpaTypes; }
            set
            {
                _tnpaTypes = value;
                OnPropertyChanged();
            }
        }

        private async void GetTnpaTypsAsync()
        {
            var colllectTnpaType = await _repository.GetTnpaTypeListAsunc();

            TnpaTypes = new ObservableCollection<TnpaTypeView>();
            foreach (var type in colllectTnpaType)
            {
                TnpaTypes.Add(new TnpaTypeView { Id = type.Id, Name = type.Name });
            }
        }

        private TnpaTypeView _selectedTnpaType;
        public TnpaTypeView SelectedTnpaType
        {
            get { return _selectedTnpaType; }
            set
            {
                _selectedTnpaType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TnpaView> _tnpas;
        public ObservableCollection<TnpaView> Tnpas
        {
            get { return _tnpas; }
            set
            {
                _tnpas = value;
                OnPropertyChanged();
            }
        }

        private async void GetTnpaAsync()
        {
            var colllectTnpa = await _repository.GetTnpaListAsunc();

            Tnpas = new ObservableCollection<TnpaView>();
            foreach (var tnpa in colllectTnpa)
            {
                Tnpas.Add(new TnpaView
                {
                    Id = tnpa.Id,
                    Number = $"{tnpa.Number}-{tnpa.Year}",
                    Name = tnpa.Name,
                    PutIntoOperation = tnpa.PutIntoOperation,
                    Cancelled = tnpa.Cancelled,
                    Registered = tnpa.Registered,
                    NumberRegistered = tnpa.NumberRegistered,
                    IsReal = tnpa.IsReal,
                    Type = tnpa.Type.Name
                });
            }

        }

        private void ShowAddTNPAWindow()
        {
            AddTNPAWindow addWindow = new AddTNPAWindow
            {
                Owner = _window
            };
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
            addWindow.ShowDialog();
            GetTnpaAsync();
        }
    }
}
