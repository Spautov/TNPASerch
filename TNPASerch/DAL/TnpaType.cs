using System.Collections.Generic;

namespace DAL
{
    public class TnpaType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tnpa> Tnpas { get; set; }

        public TnpaType()
        {
            Tnpas = new List<Tnpa>();
        }
    }
}