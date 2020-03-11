using DAL;
using System;
using System.Windows;

namespace TNPASerch.ViewModel
{
    public class AddTNPAViewModel : BaseTNPAViewModel
    {
        public AddTNPAViewModel(): base()
        {
            GetTnpaTypsAsync();
            Title = "Добавить ТНПА";
            _currentTnpa = new Tnpa();
            CountChanges = _currentTnpa.Changes.Count;
            YearTnpa = "";
            NumberTnpa = "";
            TnpaName = "";
            
            PutIntoOperationTnpa = DateTime.Now;
            CancelledTnpa = new DateTime();
            Registered = DateTime.Now;
        }
         
        protected override void Save()
        {
            if (CreatTnpa())
            {
               Close();
            }
        }

        protected override void Apply()
        {
            if (CreatTnpa())
            {
                _currentTnpa = new Tnpa();
                YearTnpa = "";
                NumberTnpa = "";
                TnpaName = "";
                NumberRegisteredTnpa = 0;
                SelectedTnpaType = null;
                CountChanges = _currentTnpa.Changes.Count;
                IsValid = false;

                PutIntoOperationTnpa = DateTime.Now;
                CancelledTnpa = new DateTime();
                Registered = DateTime.Now;
            }
        }

        private bool CreatTnpa()
        {
            if (!СheckFild())
            {
                return false;
            }
            _currentTnpa.Year = int.Parse(YearTnpa);
            _currentTnpa.Cancelled = CancelledTnpa;
            _currentTnpa.Registered = Registered;

            try
            {
                _repository.Create(_currentTnpa);
                YesMessage($"{_currentTnpa.Type.Name} {_currentTnpa.Number} - {_currentTnpa.Year} успешно добавлен");
            }
            catch (Exception ex)
            {
                YesMessage(ex.Message, "Ошибка");
                return false;
            }
            return true;
        }
    }
}
