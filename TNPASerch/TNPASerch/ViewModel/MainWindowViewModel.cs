using DAL;
using DbWorker;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
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
using TNPASerch.Model;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly MainWindow _window;

        private readonly TnpaDbContext _dbContext;
        public ICommand ShowAddTNPAWindowCommand { get; set; }
        public ICommand ShowTNPATypeEditWindowCommand { get; set; }
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
            ShowTNPATypeEditWindowCommand = new RelayCommand(ShowTNPATypeEditWindow);
            SearchCommand = new RelayCommand(SearchAsync);
            CloseCommand = new RelayCommand(Close);
        }

        private void ShowTNPATypeEditWindow()
        {
            TnpaTypeEditView tnpaTypeEditView = new TnpaTypeEditView();
            tnpaTypeEditView.DataContext = new TnpaTypeEditViewModel(tnpaTypeEditView);
            tnpaTypeEditView.ShowDialog();

            GetTnpaTypsAsync();
            GetTnpaAsync();
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

        private void GetTnpaTyps()
        {
            lock (_lockDb)
            {
                var colllectTnpaType = _dbContext.TnpaTypes.Select(x => new TnpaTypeView {Id = x.Id, Name = x.Name });
                if (colllectTnpaType.Count() > 0)
                {
                    TnpaTypes = new ObservableCollection<TnpaTypeView>(colllectTnpaType.ToList());
                }
            }
        }

        private async void GetTnpaTypsAsync()
        {
            await Task.Run(() => GetTnpaTyps());
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

        private void GetTnpa()
        {
            lock (_lockDb)
            {
                var colllectTnpa = _dbContext.Tnpas.Include(x => x.Type).Select(t =>
                new TnpaView {Id = t.Id,
                Number = $"{t.Number}-{t.Year}",
                Name = t.Name,
                PutIntoOperation = t.PutIntoOperation,
                Cancelled = t.Cancelled,
                Registered = t.Registered,
                NumberRegistered = t.NumberRegistered,
                IsReal = t.IsReal, 
                Type = t.Type.Name
                });

                if (colllectTnpa.Count()>0)
                {
                    Tnpas = new ObservableCollection<TnpaView>(colllectTnpa.ToList());
                }
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
            AddTNPAWindow addWindow = new AddTNPAWindow
            {
                Owner = _window
            };
            addWindow.DataContext = new AddTNPAViewModel(addWindow);
            addWindow.ShowDialog();
            GetTnpaAsync();
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
