﻿namespace DAL
{
    /// <summary>
    /// Класс для описания файла - содержащего текст документа
    /// </summary>
    public class DataFileInfo
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Хэш документа
        /// </summary>
        public int HashCode { get; set; }

        public int TnpaId { get; set; }
        /// <summary>
        /// ТНПА которому принадлежит файл
        /// </summary>
        public Tnpa Tnpa { get; set; }
    }
}
