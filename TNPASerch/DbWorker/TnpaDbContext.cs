using DAL;
using Microsoft.EntityFrameworkCore;

namespace DbWorker
{
    public class TnpaDbContext: DbContext
    {
        public DbSet<Tnpa> Tnpas { get; set; }
        public DbSet<TnpaType> TnpaTypes { get; set; }
        public DbSet<FolderHashCod> FolderHashCods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=DataBase\TNPA.db");
        }
    }
}
