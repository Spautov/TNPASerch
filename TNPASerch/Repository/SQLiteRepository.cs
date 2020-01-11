using DAL;
using DbWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SQLiteRepository : IRepository
    {
        private readonly TnpaDbContext _dbContext;
        public SQLiteRepository()
        {
            _dbContext = new TnpaDbContext();
        }
        public void Create(Tnpa item)
        {
            throw new NotImplementedException();
        }

        public void Create(TnpaType item)
        {
            throw new NotImplementedException();
        }

        public void DeleteTnpa(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTnpaType(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Tnpa GetTnpa(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tnpa> GetTnpaList()
        {
            throw new NotImplementedException();
        }

        public TnpaType GetTnpaType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TnpaType> GetTnpaTypeList()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Tnpa item)
        {
            throw new NotImplementedException();
        }

        public void Update(TnpaType item)
        {
            throw new NotImplementedException();
        }
    }
}
