using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tnpa
    {
        public int Id { get; set; }
        public string Namber { get; set; }
        public string Name { get; set; }
        public DateTime PutIntoOperation { get; set; }
        public DateTime Cancelled { get; set; }

        public int TnpaTypeId { get; set; }
        public TnpaType Type { get; set; }

        public ICollection<Change> Changes { get; set; }

        public Tnpa()
        {
            Changes = new List<Change>();
        }
    }
}
