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
                YesMessage($"ТНПА {_currentTnpa.Type.Name} {_currentTnpa.Number} успешно добавлен");
            }
            catch (Exception ex)
            {
                YesMessage(ex.Message, "Ошибка");
                return false;
            }
            return true;
        }

        private bool СheckFild()
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
                if (YearTnpa.Length<2 || YearTnpa.Length>4)
                {
                    throw new Exception("Год ТНПА введен в неверном формате");
                }
                try
                {
                    int year = int.Parse(YearTnpa);
                    var nowyear = DateTime.Now.Year;

                    if (year <100)
                    {
                        if (year < 50)
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        if (year<2000 || year > nowyear)
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
                var Namber = NumberTnpa + "-" + YearTnpa;
                var tnpas = _repository.FindTnpaByNumber(Namber);
                if (tnpas != null && tnpas.TnpaTypeId == SelectedTnpaType.Id)
                {
                    throw new Exception($"ТНПА {SelectedTnpaType.Name} {Namber} уже зарегистрирован в журнале под № {tnpas.NumberRegistered}");
                }
                return true;
            }
            catch (Exception ex)
            {
                YesMessage(ex.Message);
                return false;
            }
        }
    }
}
