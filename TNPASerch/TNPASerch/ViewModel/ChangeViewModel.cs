using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class ChangeViewModel : BaseViewModel
    {
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public Change Resoult { get; set; }

        private readonly Tnpa _tnpa;

        public ChangeViewModel(ChangeView window, Tnpa tnpa) : base(window)
        {
            _tnpa = tnpa ?? throw new ArgumentNullException(nameof(tnpa));

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Close);
            Resoult = null;
            Registered = DateTime.Now;
            PutIntoOperation = DateTime.Now;
            NumberChange = 0;
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
        private void Ok()
        {
            if (NumberChange < 1)
            {
                YesMessage("Некорректный номер изменения", "Ошибка");
                return;
            }
            if (Chek())
            {
                YesMessage("Изменение с таким номером уже существует", "Ошибка");
                return;
            }
            Resoult = new Change
            {
                Number = this.NumberChange,
                PutIntoOperation = this.PutIntoOperation,
                Registered = this.Registered,
                Tnpa = _tnpa,
            };
            Close();
        }

        private bool Chek()
        {
            var coolect = _tnpa.Changes.Where(ch => ch.Number == NumberChange);
            if (coolect.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
