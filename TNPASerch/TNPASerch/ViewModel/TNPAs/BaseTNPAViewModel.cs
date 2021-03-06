﻿using DAL;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Repositories;
using Searcher;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public abstract class BaseTNPAViewModel : BaseViewModel, IDisposable
    {
        protected readonly IRepository _repository;
        protected readonly ISearcher _searcher;
        protected Tnpa _currentTnpa;

        public ICommand SaveCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand EditChangesCommand { get; set; }
        public ICommand ElectronicVersionCommand { get; set; }

        public BaseTNPAViewModel() 
        {
            _repository = App.Container.Get<IRepository>();
            _searcher = App.Container.Get<ISearcher>();
            SaveCommand = new RelayCommand(Save);
            ApplyCommand = new RelayCommand(Apply);
            EditChangesCommand = new RelayCommand(EditChanges);
            ElectronicVersionCommand = new RelayCommand(ElectronicVersion);
        }

        public string Title { get; set; }

        private ObservableCollection<TnpaType> _tnpaTypes;
        public ObservableCollection<TnpaType> TnpaTypes
        {
            get { return _tnpaTypes; }
            set
            {
                _tnpaTypes = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get { return _currentTnpa.IsReal; }
            set
            {
                _currentTnpa.IsReal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(VvisibleCancelledTnpa));
            }
        }

        public bool VvisibleCancelledTnpa
        {
            get
            {
                return !_currentTnpa.IsReal;
            }
        }

        private int _countChanges;
        public int CountChanges
        {
            get { return _countChanges; }
            set
            {
                _countChanges = value;
                OnPropertyChanged();
            }
        }

        public TnpaType SelectedTnpaType
        {
            get { return _currentTnpa.Type; }
            set
            {
                _currentTnpa.Type = value;
                OnPropertyChanged();
            }
        }

        public string _numberTnpa;
        public string NumberTnpa
        {
            get { return _currentTnpa.Number; }
            set
            {
                _currentTnpa.Number = value;
                OnPropertyChanged();
            }
        }

        public int NumberRegisteredTnpa
        {
            get { return _currentTnpa.NumberRegistered; }
            set
            {
                _currentTnpa.NumberRegistered = value;
                OnPropertyChanged();
            }
        }

        public string _yearTnpa;
        public string YearTnpa
        {
            get { return _yearTnpa; }
            set
            {
                _yearTnpa = value;
                OnPropertyChanged();
            }
        }

        public DateTime PutIntoOperationTnpa
        {
            get { return _currentTnpa.PutIntoOperation; }
            set
            {
                _currentTnpa.PutIntoOperation = value;
                OnPropertyChanged();
            }
        }

        public DateTime _cancelledTnpa;
        public DateTime CancelledTnpa
        {
            get { return _cancelledTnpa; }
            set
            {
                _cancelledTnpa = value;
                OnPropertyChanged();
            }
        }

        public DateTime _registered;
        public DateTime Registered
        {
            get { return _registered; }
            set
            {
                _registered = value;
                OnPropertyChanged();
            }
        }

        public string TnpaName
        {
            get { return _currentTnpa.Name; }
            set
            {
                _currentTnpa.Name = value;
                OnPropertyChanged();
            }
        }

        private void ElectronicVersion()
        {
            var view = ViewsManager.EditFilesView(_currentTnpa);
            view.ShowDialog();
        }

        protected void EditChanges()
        {
            var view = ViewsManager.TnpaChengesEditView(_currentTnpa); 
            view.ShowDialog();
            CountChanges = _currentTnpa.Changes.Count;
        }

        protected abstract void Apply();
        protected abstract void Save();
        
        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async void GetTnpaTypsAsync()
        {
            var colllectTnpaType = await _repository.GetTnpaTypeListAsunc();
            TnpaTypes = new ObservableCollection<TnpaType>(colllectTnpaType);
        }

        protected bool СheckFild()
        {
            try
            {
                if (SelectedTnpaType == null)
                {
                    throw new Exception("Не выбран тип ТНПА");
                }

                NumberTnpa = NumberTnpa.Trim(' ', '-');

                if (String.IsNullOrEmpty(NumberTnpa) || String.IsNullOrWhiteSpace(NumberTnpa))
                {
                    throw new Exception("Не введен номер ТНПА");
                }
                NumberTnpa = NumberTnpa.Replace(',', '.');

                YearTnpa = YearTnpa.Trim(' ');
                if (String.IsNullOrEmpty(YearTnpa) || String.IsNullOrWhiteSpace(YearTnpa))
                {
                    throw new Exception("Не введен год ТНПА");
                }
                if (YearTnpa.Length < 2 || YearTnpa.Length > 4)
                {
                    throw new Exception("Год ТНПА введен в неверном формате");
                }
                try
                {
                    int year = int.Parse(YearTnpa);
                    var nowyear = DateTime.Now.Year;

                    if (year < 100)
                    {
                        if (year < 50)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        if (year < 2000 || year > nowyear)
                        {
                            throw new Exception();
                        }
                    }
                }
                catch (Exception)
                {

                    throw new Exception("Год ТНПА введен в неверном формате");
                }


                TnpaName = TnpaName.Trim(' ');
                if (String.IsNullOrEmpty(YearTnpa) || String.IsNullOrWhiteSpace(YearTnpa))
                {
                    throw new Exception("Не заполнено наименование ТНПА");
                }

                if (NumberRegisteredTnpa == 0)
                {
                    throw new Exception("Неверный номер регистрации в журнале");
                }
                
                var tnpas = _repository.FindTnpaByNumber(_currentTnpa.Number);
                foreach (var tnpa in tnpas)
                {
                    if (tnpa.TnpaTypeId == SelectedTnpaType.Id && tnpa.Year == int.Parse(YearTnpa)
                        && tnpa.Id != _currentTnpa.Id)
                    {
                        throw new Exception($"ТНПА {tnpa.Type.Name} {tnpa.Number} - {tnpa.Year} " +
                            $"уже зарегистрирован в журнале под № {tnpa.NumberRegistered}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                YesMessage(ex.Message);
                return false;
            }
        }

        protected void Close()
        {
            Window windowActiv = null;
            foreach (Window wind in App.Current.Windows)
            {
                if (wind.IsActive)
                {
                    windowActiv = wind;
                    break;
                }
            }

            if (windowActiv != null)
            {
                windowActiv.Close();
            }
        }
    }
}
