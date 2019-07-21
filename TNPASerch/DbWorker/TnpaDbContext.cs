using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker
{
    public class TnpaDbContext: DbContext
    {
        public TnpaDbContext()
            :base("Connection")
        {

        }

        public DbSet<Tnpa> Tnpas { get; set; }
        public DbSet<TnpaType> TnpaTypes { get; set; }
    }
}
