﻿using DAL;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Repositories;
using Searcher;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.Model;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IRepository _repository;
        protected readonly ISearcher _searcher;
        private readonly IFileRepository _fileRepository;

        public ICommand ShowAddTNPAWindowCommand { get; set; }
        public ICommand ShowEditTNPAWindowCommand { get; set; }
        public ICommand ShowTNPATypeEditWindowCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CleanQueueCommand { get; set; }
        public ICommand DeleteTNPACommand { get; set; }


        public MainWindowViewModel()
        {
            _repository = App.Container.Get<IRepository>();
            _searcher = App.Container.Get<ISearcher>();
            _fileRepository = App.Container.Get<IFileRepository>();
            GetTnpaAsync();
            ShowAddTNPAWindowCommand = new RelayCommand(ShowAddTNPAWindow);
            ShowEditTNPAWindowCommand = new RelayCommand(ShowEditTNPAWindow);
            ShowTNPATypeEditWindowCommand = new RelayCommand(ShowTNPATypeEditWindow);
            SearchCommand = new RelayCommand(SearchAsync);
            CleanQueueCommand = new RelayCommand(CleanQueue);
            DeleteTNPACommand = new RelayCommand(DeleteTNPA);
        }

        private void DeleteTNPA()
        {
            var nameTNPA = $"{SelectedTnpa.Type} {SelectedTnpa.Number}";
            var answer = YesNoCancelMessage($"Вы точно желаете удалит {nameTNPA}?", "Удаление");
            if (answer)
            {
                var tnpa = _repository.GetTnpa(SelectedTnpa.Id);
                try
                {
                    _searcher.Remove(tnpa);
                }
                catch (Exception) { }
               
                if (tnpa.Files.Count > 0)
                {
                    foreach (var file in tnpa.Files)
                    {
                        try
                        {
                            _fileRepository.RemoveFile(file.Path);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                _repository.DeleteTnpa(SelectedTnpa.Id);
                YesMessage($"{nameTNPA} успешно удален");
                GetTnpaAsync();
            }
        }

        private void CleanQueue()
        {
            GetTnpaAsync();
        }

        private void ShowEditTNPAWindow()
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

        public bool IsSelectedTnpa
        {
            get { return SelectedTnpa != null; }
        }

        public bool IsSearchCommandEnabled
        {
            get { return !string.IsNullOrEmpty(NumberTnpa) && !string.IsNullOrWhiteSpace(NumberTnpa); }
        }

        private void ShowTNPATypeEditWindow()
        {
            TnpaTypeEditView tnpaTypeEditView = ViewsManager.TnpaTypeEditView();
            tnpaTypeEditView.ShowDialog();

            GetTnpaAsync();
        }

        private async void SearchAsync()
        {
            await Task.Run(() => { Search(); });
        }

        private void Search()
        {
            NumberTnpa = NumberTnpa.Trim(' ');
            NumberTnpa = NumberTnpa.ToLower();
            var collect = _searcher.Serch(NumberTnpa);
            Tnpas = TnpaToTnpaView(collect);
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
                OnPropertyChanged(nameof(IsSearchCommandEnabled));
            }
        }

        private TnpaTypeViewModel _selectedTnpaType;
        public TnpaTypeViewModel SelectedTnpaType
        {
            get { return _selectedTnpaType; }
            set
            {
                _selectedTnpaType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TnpaViewModel> _tnpas;
        public ObservableCollection<TnpaViewModel> Tnpas
        {
            get { return _tnpas; }
            set
            {
                _tnpas = value;
                OnPropertyChanged();
            }
        }

        private TnpaViewModel _selectedTnpa;
        public TnpaViewModel SelectedTnpa
        {
            get { return _selectedTnpa; }
            set
            {
                _selectedTnpa = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedTnpa));
            }
        }


        private async void GetTnpaAsync()
        {
            var colllectTnpa = await _repository.GetTnpaListAsunc();
            
            Tnpas = TnpaToTnpaView(colllectTnpa.Reverse().Take(5));
        }

        private ObservableCollection<TnpaViewModel> TnpaToTnpaView(IEnumerable<Tnpa> colllectTnpa)
        {
            var tmp = new ObservableCollection<TnpaViewModel>();
            foreach (var tnpa in colllectTnpa)
            {
                tmp.Add(new TnpaViewModel
                {
                    Id = tnpa.Id,
                    Number = $"{tnpa.Number}-{tnpa.Year}",
                    Name = tnpa.Name,
                    PutIntoOperation = tnpa.PutIntoOperation.ToString("dd-MM-yyyy"),
                    Cancelled = tnpa.Cancelled.ToString("dd-MM-yyyy"),
                    Registered = tnpa.Registered.ToString("dd-MM-yyyy"),
                    NumberRegistered = tnpa.NumberRegistered,
                    IsReal = tnpa.IsReal,
                    Type = tnpa.Type.Name,
                    Changes = tnpa.Changes,
                    Files = tnpa.Files,
                    Content = tnpa.Content
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
