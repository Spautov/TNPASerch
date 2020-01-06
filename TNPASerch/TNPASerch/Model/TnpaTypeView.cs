﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TNPASerch.Model
{
    /// <summary>
    /// Класс для отображения тип ТНПА
    /// </summary>
    public class TnpaTypeView : INotifyPropertyChanged
    {
        private int _id;
        /// <summary>
        /// Id тип ТНПА
        /// </summary>
        public int Id
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        /// <summary>
        /// Наименование типа ТНПА
        /// </summary>
        public string Name 
        {
            get { return _name; } 
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
