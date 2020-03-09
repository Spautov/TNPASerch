using DAL;
using System;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class EditTNPAViewModel : BaseTNPAViewModel
    {
        public EditTNPAViewModel(TNPAWindow window, Tnpa tnpa) : base(window)
        {
            _currentTnpa = tnpa?? throw new ArgumentNullException(nameof(tnpa));

            GetTnpaTypsAsync();

            YearTnpa = _currentTnpa.Year.ToString();
            CountChanges = _currentTnpa.Changes.Count;

            PutIntoOperationTnpa = _currentTnpa.PutIntoOperation;
            CancelledTnpa = _currentTnpa.Cancelled;
            Registered = _currentTnpa.Registered;
        }

        protected override void Apply()
        {
            UpdateTnpa();
        }

        protected override void Save()
        {
            if (UpdateTnpa())
            {
                Close();
            };
        }

        private bool UpdateTnpa()
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
                _repository.Update(_currentTnpa);
                YesMessage($"{_currentTnpa.Type.Name} {_currentTnpa.Number} - {_currentTnpa.Year} успешно обнавлен");
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
