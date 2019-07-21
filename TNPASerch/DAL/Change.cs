using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Change
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime PutIntoOperation { get; set; }

        public int TnpaId { get; set; }
        public Tnpa Tnpa { get; set; }
    }
}