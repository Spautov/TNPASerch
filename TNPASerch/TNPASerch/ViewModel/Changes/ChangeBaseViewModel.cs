using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public abstract class ChangeBaseViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }

        public string Title { get; protected set; }

        protected readonly Tnpa _tnpa;

        public ChangeBaseViewModel(Tnpa tnpa)
        {
            _tnpa = tnpa ?? throw new ArgumentNullException(nameof(tnpa));

            OkCommand = new RelayCommand(Ok);
        }

        public DateTime _putIntoOperation;
        public DateTime PutIntoOperation
        {
            get { return _putIntoOperation; }
            set
            {
                _putIntoOperation = value;
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

        private int _numberChange;
        public int NumberChange
        {
            get { return _numberChange; }
            set
            {
                _numberChange = value;
                OnPropertyChanged();
            }
        }
        protected abstract void Ok();

        
    }
}
