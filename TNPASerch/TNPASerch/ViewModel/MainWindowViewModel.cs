using DAL;
using DbWorker;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MainWindow _window;

        private readonly TnpaDbContext _dbContext;
        public ICommand ShowAddTNPAWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private readonly object _lockDb;

        public MainWindowViewModel(MainWindow window)
        {
            _window = window ?? throw new ArgumentNullException("window");
            _dbContext = new TnpaDbContext();
            _lockDb = new object();
            GetTnpaTypsAsync();
            GetTnpaAsync();
            ShowAddTNPAWindowCommand = new RelayCommand(ShowAddTNPAWindow);
            SearchCommand = new RelayCommand(SearchAsync);
            CloseCommand = new RelayCommand(Close);
        }

        private async void SearchAsync()
        {
            await Task.Run(()=> { Search(); });
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

        public ObservableCollection<TnpaType> _tnpaTypes;
        public ObservableCollection<TnpaType> TnpaTypes
        {
            get { return _tnpaTypes; }
            set
            {
                _tnpaTypes = value;
                OnPropertyChanged();
            }
        }

        private void GetTnpaTyps()
        {
            lock (_lockDb)
            {
                var colllectTnpaType = _dbContext.TnpaTypes.Select(x => x);
                TnpaTypes = new ObservableCollection<TnpaType>(colllectTnpaType.ToList());
            }
        }

        private async void GetTnpaTypsAsync()
        {
            await Task.Run(() => GetTnpaTyps());
        }

        public TnpaType _selectedTnpaType;
        public TnpaType SelectedTnpaType
        {
            get { return _selectedTnpaType; }
            set
            {
                _selectedTnpaType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Tnpa> _tnpas;
        public ObservableCollection<Tnpa> Tnpas
        {
            get { return _tnpas; }
            set
            {
                _tnpas = value;
                OnPropertyChanged();
            }
        }

        private void GetTnpa()
        {
            lock (_lockDb)
            {
                var colllectTnpa = _dbContext.Tnpas.Select(x => x);
                Tnpas = new ObservableCollection<Tnpa>(colllectTnpa.ToList());
            }
        }

        private async void GetTnpaAsync()
        {
            await Task.Run(() => GetTnpa());
        }

        private void Close()
        {
            _window.Close();
        }

        private void ShowAddTNPAWindow()
        {
            AddTNPAWindow addWindow = new AddTNPAWindow();
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
            addWindow.Show();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
