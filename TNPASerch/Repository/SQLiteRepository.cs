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
        private readonly object _lockDb;

        public static SQLiteRepository GetRepository()
        {
            if (_SQLiteRepository == null)
                _SQLiteRepository = new SQLiteRepository();
            return _SQLiteRepository;
        }

        private SQLiteRepository()
        {
            _lockDb = new object();
            _dbContext = new TnpaDbContext();
        }
        public void Create(Tnpa item)
        {
            lock (_lockDb)
            {
                _dbContext.Tnpas.Add(item);
                Save();
            }
        }

        public void Create(TnpaType item)
        {
            lock (_lockDb)
            {
                _dbContext.TnpaTypes.Add(item);
                Save();
            }
        }

        public void DeleteTnpa(int id)
        {
            lock (_lockDb)
            {
                Tnpa tnpa = _dbContext.Tnpas.Find(id);
                if (tnpa != null)
                    _dbContext.Tnpas.Remove(tnpa);
            }
        }

        public void DeleteTnpaType(int id)
        {
            lock (_lockDb)
            {
                TnpaType tnpaType = _dbContext.TnpaTypes.Find(id);
                if (tnpaType != null)
                    _dbContext.TnpaTypes.Remove(tnpaType);
            }
        }

        public Tnpa GetTnpa(int id)
        {
            lock (_lockDb)
            {
                return _dbContext.Tnpas.Find(id);
            }
        }

        public IEnumerable<Tnpa> GetTnpaList()
        {
            lock (_lockDb)
            {
                return _dbContext.Tnpas;
            }
        }

        async public ValueTask<IEnumerable<Tnpa>> GetTnpaListAsunc()
        {
            var resoult = await Task.Run(() => {
                lock (_lockDb)
                { 
                    return _dbContext.Tnpas; 
                }
            });
            return resoult;
        }

        public TnpaType GetTnpaType(int id)
        {
            lock (_lockDb)
            {
                return _dbContext.TnpaTypes.Find(id);
            }
        }

        async public ValueTask<IEnumerable<TnpaType>> GetTnpaTypeListAsunc()
        {
            var resoult = await Task.Run(() => {
                lock (_lockDb)
                {
                    return _dbContext.TnpaTypes;
                }
            });
            return resoult;
        }

        public IEnumerable<TnpaType> GetTnpaTypeList()
        {
            lock (_lockDb)
            {
                return _dbContext.TnpaTypes;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Tnpa item)
        {
            lock (_lockDb)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                Save();
            }
        }

        public void Update(TnpaType item)
        {
            lock (_lockDb)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                Save();
            }
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
