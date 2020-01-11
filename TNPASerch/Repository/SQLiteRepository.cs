using DAL;
using DbWorker;
using Microsoft.EntityFrameworkCore;
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

        private static SQLiteRepository _SQLiteRepository;

        public static SQLiteRepository GetRepository()
        {
            if (_SQLiteRepository == null)
                _SQLiteRepository = new SQLiteRepository();
            return _SQLiteRepository;
        }

        private SQLiteRepository()
        {
            _dbContext = new TnpaDbContext();
        }
        public void Create(Tnpa item)
        {
            _dbContext.Tnpas.Add(item);
            Save();
        }

        public void Create(TnpaType item)
        {
            _dbContext.TnpaTypes.Add(item);
            Save();
        }

        public void DeleteTnpa(int id)
        {
            Tnpa tnpa = _dbContext.Tnpas.Find(id);
            if (tnpa != null)
                _dbContext.Tnpas.Remove(tnpa);
        }

        public void DeleteTnpaType(int id)
        {
            TnpaType tnpaType = _dbContext.TnpaTypes.Find(id);
            if (tnpaType != null)
                _dbContext.TnpaTypes.Remove(tnpaType);
        }

        public Tnpa GetTnpa(int id)
        {
            return _dbContext.Tnpas.Find(id);
        }

        public IEnumerable<Tnpa> GetTnpaList()
        {
            return _dbContext.Tnpas;
        }

        async public ValueTask<IEnumerable<Tnpa>> GetTnpaListAsunc()
        {
            var resoult = await Task.Run(() => _dbContext.Tnpas);
            return resoult;
        }

        public TnpaType GetTnpaType(int id)
        {
            return _dbContext.TnpaTypes.Find(id);
        }

        async public ValueTask<IEnumerable<TnpaType>> GetTnpaTypeListAsunc()
        {
            var resoult = await Task.Run(() => _dbContext.TnpaTypes);
            return resoult;
        }

        public IEnumerable<TnpaType> GetTnpaTypeList()
        {
            return _dbContext.TnpaTypes;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Tnpa item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            Save();
        }

        public void Update(TnpaType item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            Save();
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
