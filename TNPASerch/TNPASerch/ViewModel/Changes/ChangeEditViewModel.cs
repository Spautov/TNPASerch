using DAL;
using System.Linq;
using TNPASerch.View;

namespace TNPASerch.ViewModel
{
    public class ChangeEditViewModel : ChangeBaseViewModel
    {
        private readonly Change _currentChange;

        public ChangeEditViewModel(ChangeView window, Tnpa tnpa, int number) : base(window, tnpa)
        {
            var coolect = _tnpa.Changes.Where(ch => ch.Number == number);
            if (coolect.Count() > 0)
                _currentChange = coolect.First();

            Registered = _currentChange.Registered;
            PutIntoOperation = _currentChange.PutIntoOperation;
            NumberChange = _currentChange.Number;
            Title = $"Редактировать изменение № {_currentChange.Number}";
        }

        protected override void Ok()
        {
            if (NumberChange < 1)
            {
                YesMessage("Некорректный номер изменения", "Ошибка");
                return;
            }
            if (!Chek())
            {
                YesMessage("Изменение с таким номером уже существует", "Ошибка");
                return;
            }
            _currentChange.Number = this.NumberChange;
            _currentChange.PutIntoOperation = this.PutIntoOperation;
            _currentChange.Registered = this.Registered;
            //Close();
        }

        private bool Chek()
        {
            var coolect = _tnpa.Changes.Where(ch => ch.Number == NumberChange && ch.Number!= _currentChange.Number);
            if (coolect.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
