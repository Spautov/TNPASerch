using DAL;
using DbWorker;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTNPAViewModel :IDisposable
    {
        private readonly AddTNPAWindow _window;

        private readonly TnpaDbContext _dbContext;
        public bool IsValid { get; set; }
        public TnpaType SelectedTnpaType { get; set; }
        public string NumberTnpa { get; set; }
        public string YearTnpa { get; set; }
        public DateTime PutIntoOperationTnpa { get; set; }
        public DateTime CancelledTnpa { get; set; }
        public ObservableCollection<TnpaType> TnpaTypes { get; set; }

        public AddTNPAViewModel(AddTNPAWindow window)
        {
            _window = window;
            _dbContext = new TnpaDbContext();
            var colllectTnpaType = _dbContext.TnpaTypes.Select(x => x);
            TnpaTypes = new ObservableCollection<TnpaType>(colllectTnpaType.ToList());
            SaveCommand = new RelayCommand(Save);
            ApplyCommand = new RelayCommand(Apply);
            CancelCommand = new RelayCommand(Cancel);
            YearTnpa = "Введите год ТНПА";
            NumberTnpa = "Введите номер ТНПА";
            PutIntoOperationTnpa = DateTime.Now;
        }

        private void Save()
        {
            Tnpa tnpa = new Tnpa
            {
                Type = SelectedTnpaType,
                Namber = NumberTnpa +"-"+ YearTnpa,
                Name = "Новое ТНПА",
                PutIntoOperation = PutIntoOperationTnpa,
                Cancelled = new DateTime(),
                Registered = DateTime.Now
            };
            _dbContext.Tnpas.Add(tnpa);
            _dbContext.SaveChanges();
            _window.Close();
        }

        private void Apply()
        {
            Tnpa tnpa = new Tnpa
            {
                Type = SelectedTnpaType,
                Namber = NumberTnpa + "-" + YearTnpa,
                Name = "Новое ТНПА",
                PutIntoOperation = PutIntoOperationTnpa,
                Cancelled = new DateTime(),
                Registered = DateTime.Now
            };
            _dbContext.Tnpas.Add(tnpa);
            _dbContext.SaveChanges();
            SelectedTnpaType = null;
            NumberTnpa = " ";
            YearTnpa = "";
            PutIntoOperationTnpa = DateTime.Now;
            IsValid = false;

        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        private void Cancel()
        {
            _window.Close();
        }

        public ICommand SaveCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        
    }
}
