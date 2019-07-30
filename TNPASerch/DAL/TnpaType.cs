using System.Collections.Generic;

namespace DAL
{
    /// <summary>
    /// Класс описывающий тип ТНПА
    /// </summary>
    public class TnpaType
    {
        /// <summary>
        /// Id тип ТНПА
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование типа ТНПА
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция ТНПА данного типа
        /// </summary>
        public ICollection<Tnpa> Tnpas { get; set; }

        public TnpaType()
        {
            Tnpas = new List<Tnpa>();
        }
    }
}