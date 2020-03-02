using DAL;
using GalaSoft.MvvmLight.Command;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ICommand ShowEditTNPAWindowCommand { get; set; }
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
            ShowEditTNPAWindowCommand = new RelayCommand(ShowEditTNPAWindow);
            ShowTNPATypeEditWindowCommand = new RelayCommand(ShowTNPATypeEditWindow);
            SearchCommand = new RelayCommand(SearchAsync);
            CloseCommand = new RelayCommand(Close);
        }

        private void ShowEditTNPAWindow()
        {
            if (SelectedTnpa != null)
            {
                var tnpa = _repository.GetTnpa(SelectedTnpa.Id);
                if (tnpa == null)
                {
                    return;
                }
                TNPAWindow addWindow = ViewsManager.EditTNPAView(tnpa);
                addWindow.ShowDialog();

                GetTnpaAsync();
            }
        }

        private void ShowTNPATypeEditWindow()
        {
            TnpaTypeEditView tnpaTypeEditView = ViewsManager.TnpaTypeEditView();
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
            NumberTnpa = NumberTnpa.Trim(' ');

            int findnum = 0;
            while (findnum != -1)
            {
                findnum = NumberTnpa.IndexOf(' ');
                if (findnum != -1)
                {
                    NumberTnpa = NumberTnpa.Remove(findnum,1);
                }
            }
            
            if (String.IsNullOrEmpty(NumberTnpa) || String.IsNullOrWhiteSpace(NumberTnpa) || SelectedTnpaType == null)
            {
                return;
            }
            NumberTnpa = NumberTnpa.Replace(',', '.');
            var collect = _repository.SearchTnpaByNumber(NumberTnpa).Where(el => el.TnpaTypeId == SelectedTnpaType.Id);

            Tnpas = TnpaToTnpaView(collect);
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
            SelectedTnpaType = TnpaTypes.First();
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

        private TnpaView _selectedTnpa;
        public TnpaView SelectedTnpa
        {
            get { return _selectedTnpa; }
            set 
            { 
                _selectedTnpa = value;
                OnPropertyChanged();
            }
        }


        private async void GetTnpaAsync()
        {
            var colllectTnpa = await _repository.GetTnpaListAsunc();

            Tnpas = TnpaToTnpaView(colllectTnpa);
        }

        private ObservableCollection<TnpaView> TnpaToTnpaView(IEnumerable<Tnpa> colllectTnpa)
        {
            var tmp = new ObservableCollection<TnpaView>();
            foreach (var tnpa in colllectTnpa)
            {
                tmp.Add(new TnpaView
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
            return tmp;
        }

        private void ShowAddTNPAWindow()
        {
            TNPAWindow addWindow = ViewsManager.AddTNPAView();
            addWindow.ShowDialog();

            GetTnpaAsync();
        }
    }
}
