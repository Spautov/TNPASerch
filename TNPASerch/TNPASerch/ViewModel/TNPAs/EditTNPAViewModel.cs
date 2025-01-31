using DAL;
using System;
using System.Threading.Tasks;

namespace TNPASerch.ViewModel
{
    public class EditTNPAViewModel : BaseTNPAViewModel
    {
        public EditTNPAViewModel(Tnpa tnpa) : base()
        {
            _currentTnpa = tnpa?? throw new ArgumentNullException(nameof(tnpa));

            GetTnpaTypsAsync();

            YearTnpa = _currentTnpa.Year.ToString();
            CountChanges = _currentTnpa.Changes.Count;

            PutIntoOperationTnpa = _currentTnpa.PutIntoOperation;
            CancelledTnpa = _currentTnpa.Cancelled;
            Registered = _currentTnpa.Registered;
        }

        protected override async Task ApplyAsync()
        {
            await UpdateTnpaAsync();
        }

        protected override async Task SaveAsync()
        {
            if (await UpdateTnpaAsync())
            {
                Close();
            };
        }

        private async Task<bool> UpdateTnpaAsync()
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
                await _repository.UpdateAsync(_currentTnpa);
                _searcher.Remove(_currentTnpa);
                _searcher.Add(_currentTnpa);
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
