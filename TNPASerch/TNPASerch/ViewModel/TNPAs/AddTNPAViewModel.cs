using DAL;
using System;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class AddTNPAViewModel : BaseTNPAViewModel
    {
        public AddTNPAViewModel(TNPAWindow window): base(window)
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
         
        protected override void EditChanges()
        {
            var view = new TnpaChengesEditView
            {
                Owner = _window
            };

            var ViewModel = new TnpaChengesEditViewModel(view, _currentTnpa);
            view.DataContext = ViewModel;
            view.ShowDialog();
            CountChanges = _currentTnpa.Changes.Count;
        }

        protected override void Save()
        {
            if (CreatTnpa())
            {
                _window.Close();
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
