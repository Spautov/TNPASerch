﻿using DAL;
using System;
using System.Linq;
using System.Windows;

namespace TNPASerch.ViewModel
{
    public class ChangeViewModel : ChangeBaseViewModel
    {
        public ChangeViewModel(Tnpa tnpa) : base(tnpa)
        {
            Registered = DateTime.Now;
            PutIntoOperation = DateTime.Now;
            NumberChange = 0;
            Title = "Добавить изменение";
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
            var change = new Change
            {
                Number = this.NumberChange,
                PutIntoOperation = this.PutIntoOperation,
                Registered = this.Registered,
                Tnpa = _tnpa,
            };
            _tnpa.Changes.Add(change);
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

        private void Close()
        {
            Window windowActiv = null;
            foreach (Window wind in App.Current.Windows)
            {
                if (wind.IsActive)
                {
                    windowActiv = wind;
                    break;
                }
            }

            if (windowActiv != null)
            {
                windowActiv.Close();
            }
        }
    }
}
