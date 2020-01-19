using System;
using System.Globalization;

namespace DAL
{
    /// <summary>
    /// Класс описывающий изменение в ТНПА
    /// </summary>
    public class Change
    {
        /// <summary>
        /// Id изменения
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер изменения
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Дата введения в действие
        /// </summary>
        public DateTime PutIntoOperation { get; set; }
        /// <summary>
        /// Дата регистрации в журнале
        /// </summary>
        public DateTime Registered { get; set; }

        public int TnpaId { get; set; }
        /// <summary>
        /// ТНПА которому принадлежит изменение
        /// </summary>
        public Tnpa Tnpa { get; set; }

        public override string ToString()
        {
            return $"№ {Number} зарегистроирован {Registered.ToString("d", CultureInfo.CreateSpecificCulture("ru-RU"))} ";
        }
    }
}