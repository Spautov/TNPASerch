using DAL;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;
using TNPASerch.ViewModel;

namespace TNPASerch.Model
{
    public class TnpaView : NotifyPropertyChangedModel
    {
        private IRepository _repository;

        public ICommand ElectronicVersionCommand { get; set; }

        private void ElectronicVersion()
        {
            var tnpa = _repository.GetTnpa(Id);
            if (tnpa == null)
            {
                return;
            }
            var view = ViewsManager.WatchFilesView(tnpa);
            view.ShowDialog();
        }

        /// <summary>
        /// Id документа
        /// </summary>
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private int _id;
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        private string _number;

        /// <summary>
        /// Наименование документа
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _name;

        /// <summary>
        /// Дата введения в действие
        /// </summary>
        public string PutIntoOperation
        {
            get { return _putIntoOperation; }
            set
            {
                _putIntoOperation = value;
                OnPropertyChanged();
            }
        }
        private string _putIntoOperation;

        /// <summary>
        /// Дата отмены
        /// </summary>
        public string Cancelled
        {
            get { return _cancelled; }
            set
            {
                _cancelled = value;
                OnPropertyChanged();
            }
        }
        private string _cancelled;

        /// <summary>
        /// Дата регистрации в журнале
        /// </summary>
        public string Registered
        {
            get { return _registered; }
            set
            {
                _registered = value;
                OnPropertyChanged();
            }
        }
        private string _registered;

        /// <summary>
        /// Номер регистрации в журнале
        /// </summary>
        public int NumberRegistered
        {
            get { return _numberRegistered; }
            set
            {
                _numberRegistered = value;
                OnPropertyChanged();
            }
        }
        private int _numberRegistered;

        /// <summary>
        /// Действующий ТНПА
        /// </summary>
        public bool IsReal
        {
            get { return _isReal; }
            set
            {
                _isReal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCanceled));
            }
        }
        private bool _isReal;

        /// <summary>
        /// Действующий ТНПА
        /// </summary>
        public Visibility IsCanceled
        {
            get
            {
                if (_isReal)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Тип ТНПА
        /// </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        private string _type;

        /// <summary>
        /// Коллекция изменений в документ
        /// </summary>
        public ICollection<Change> Changes
        {
            get { return _changes; }
            set
            {
                _changes = value;
                OnPropertyChanged();
            }
        }
        private ICollection<Change> _changes;

        public TnpaView()
        {
            _repository = App.Container.Get<IRepository>();
            ElectronicVersionCommand = new RelayCommand(ElectronicVersion);
            Changes = new List<Change>();
        }
    }
}
