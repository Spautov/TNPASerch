using System;
using System.Collections.Generic;

namespace DAL
{
    /// <summary>
    /// Класс описывающий ТНПА
    /// </summary>
    public class Tnpa
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Namber { get; set; }
        /// <summary>
        /// Наименование документа
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата введения в действие
        /// </summary>
        public DateTime PutIntoOperation { get; set; }
        /// <summary>
        /// Дата отмены
        /// </summary>
        public DateTime Cancelled { get; set; }
        /// <summary>
        /// Дата регистрации в журнале
        /// </summary>
        public DateTime Registered { get; set; }
        /// <summary>
        /// Номер регистрации в журнале
        /// </summary>
        public int NumberRegistered { get; set; }
        /// <summary>
        /// Действующий ТНПА
        /// </summary>
        public bool IsReal { get; set; }

        public int TnpaTypeId { get; set; }
        /// <summary>
        /// Тип документа, н.п. ГОСТ, СТБ и т.п.
        /// </summary>
        public TnpaType Type { get; set; }

        /// <summary>
        /// Коллекция изменений в документ
        /// </summary>
        public ICollection<Change> Changes { get; set; }

        public Tnpa()
        {
            Changes = new List<Change>();
        }
    }
}
