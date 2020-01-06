﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TNPASerch.Model
{
    public class TnpaView : INotifyPropertyChanged
    {
        /// <summary>
        /// Id документа
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
        private int _id;
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number
        {
            get { return _number; }
            set 
            {
                _number = value;
                OnPropertyChanged();
            }
        }
        private string _number;

        /// <summary>
        /// Наименование документа
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
        private string _name;

        /// <summary>
        /// Дата введения в действие
        /// </summary>
        public DateTime PutIntoOperation 
        {
            get { return _putIntoOperation; }
            set
            {
                _putIntoOperation = value;
                OnPropertyChanged();
            }
        }
        private DateTime _putIntoOperation;

        /// <summary>
        /// Дата отмены
        /// </summary>
        public DateTime Cancelled 
        {
            get { return _cancelled; }
            set 
            {
                _cancelled = value;
                OnPropertyChanged();
            }
        }
        private DateTime _cancelled;

        /// <summary>
        /// Дата регистрации в журнале
        /// </summary>
        public DateTime Registered 
        {
            get { return _registered; } 
            set
            {
                _registered = value;
                OnPropertyChanged();
            }
        }
        private DateTime _registered;

        /// <summary>
        /// Номер регистрации в журнале
        /// </summary>
        public int NumberRegistered 
        {
            get { return _numberRegistered; }
            set
            {
                _numberRegistered = value;
                OnPropertyChanged();
            }
        }
        private int _numberRegistered;

        /// <summary>
        /// Действующий ТНПА
        /// </summary>
        public bool IsReal 
        {
            get { return _isReal; }
            set
            {
                _isReal = value;
                OnPropertyChanged();
            }
        }
        private bool _isReal;

        /// <summary>
        /// Тип ТНПА
        /// </summary>
        public string Type 
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        private string _type;

        /// <summary>
        /// Коллекция изменений в документ
        /// </summary>
        public ICollection<string> Changes 
        {
            get { return _changes; }
            set
            {
                _changes = value;
                OnPropertyChanged();
            }
        }
        private ICollection<string> _changes;

        public TnpaView()
        {
            Changes = new List<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
