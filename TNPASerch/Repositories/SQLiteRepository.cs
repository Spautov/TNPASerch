﻿using DAL;
using DbWorker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class SQLiteRepository : IRepository
    {
        private readonly TnpaDbContext _dbContext;
        private readonly object _lockDb;

        public SQLiteRepository()
        {
            _lockDb = new object();
            _dbContext = new TnpaDbContext();
        }

        public void Create(Tnpa item)
        {
            lock (_lockDb)
            {
                var collect = _dbContext.Tnpas.Where(x => x.Number.ToUpper().Equals(item.Number.ToUpper())
                && x.TnpaTypeId == item.TnpaTypeId);
                if (collect.Count() > 0)
                {
                    throw new Exception($"ТНПА {item.Type.Name} {item.Number} уже существует");
                }
                _dbContext.Tnpas.Add(item);
                Save();
            }
        }

        public void Create(TnpaType item)
        {
            lock (_lockDb)
            {
                var collect = _dbContext.TnpaTypes.Where(x => x.Name.ToUpper().Equals(item.Name.ToUpper()));
                if (collect.Count() > 0)
                {
                    throw new Exception($"Тип {item.Name} уже существует");
                }
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
                {
                    _dbContext.Tnpas.Remove(tnpa);
                    Save();
                }
            }
        }

        public void DeleteTnpaType(int id)
        {
            lock (_lockDb)
            {
                TnpaType tnpaType = _dbContext.TnpaTypes.Find(id);
                if (tnpaType != null)
                {
                    var resoulCollect = _dbContext.Tnpas.Select(x => x).Where(el => el.TnpaTypeId == tnpaType.Id);
                    if (resoulCollect.Count() > 0)
                    {
                        var message = $"Тип {tnpaType.Name} невозможно удалить, " +
                            $"так как существуют ТНПА связанные с ним. Сначала нужно удалить соответствующие ТНПА, " +
                            $"а затем тип.";
                        throw new Exception(message);
                    }
                    _dbContext.TnpaTypes.Remove(tnpaType);
                    Save();
                }
            }
        }

        public Tnpa GetTnpa(int id)
        {
            lock (_lockDb)
            {
                return _dbContext.Tnpas.Where(el => el.Id == id)
                    .Include(p => p.Changes)
                    .Include(el=> el.Files)
                    .Include(t => t.Type)
                    .ToArray()
                    .First();
            }
        }

        public IEnumerable<Tnpa> GetTnpaList()
        {
            lock (_lockDb)
            {
                return _dbContext.Tnpas.Include(p => p.Changes)
                    .Include(el => el.Files)
                    .Include(t => t.Type);
            }
        }

        async public ValueTask<IEnumerable<Tnpa>> GetTnpaListAsunc()
        {
            var resoult = await Task.Run(() =>
            {
                lock (_lockDb)
                {
                    return _dbContext.Tnpas.Include(p => p.Changes)
                    .Include(el => el.Files)
                    .Include(t => t.Type);
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
            var resoult = await Task.Run(() =>
            {
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
            Update<Tnpa>(item);
        }

        private void Update<T>(T item) where T : class
        {
            lock (_lockDb)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                Save();
            }
        }

        public void Update(TnpaType item)
        {
            Update<TnpaType>(item);
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

        public TnpaType FindTnpaTypeByName(string name)
        {
            var collect = _dbContext.TnpaTypes.Where(x => x.Name.ToUpper().Equals(name.ToUpper()));
            return collect.FirstOrDefault();
        }

        public TnpaType FindTnpaTypeById(int id)
        {
            return _dbContext.TnpaTypes.Find(id);
        }

        public Tnpa FindTnpaByName(string name)
        {
            var collect = _dbContext.Tnpas.Where(x => x.Name.ToUpper().Equals(name.ToUpper()));
            return collect.FirstOrDefault();
        }

        public Tnpa FindTnpaById(int id)
        {
            return _dbContext.Tnpas.Find(id);
        }

        public IEnumerable<Tnpa> FindTnpaByNumber(string number)
        {
            var collect = _dbContext.Tnpas.Where(x => x.Number.ToUpper().Equals(number.ToUpper()));
            return collect.ToList();
        }

        public IEnumerable<Tnpa> SearchTnpaByNumber(string number)
        {
            var numberUp = number.ToUpper();
            var collect = _dbContext.Tnpas
                .Select(x => new { id = x.Id, Number = $"{x.Number}-{x.Year}".ToString().ToUpper() })
                .ToList().Where(el => el.Number.Contains(numberUp))
                .Select(a => a.id).ToList();
            var collectTnpa = _dbContext.Tnpas.Where(r => collect.Contains(r.Id))
                .Include(p => p.Changes)
                .Include(el => el.Files)
                .ToList();
            return collectTnpa;
        }

        public FolderHashCod GetFolderHashCod()
        {
            lock (_lockDb)
            {
                return _dbContext.FolderHashCods.FirstOrDefault();
            }
        }

        public FolderHashCod CreateFolderHashCod(int hash)
        {
            FolderHashCod resoult = null;

            lock (_lockDb)
            {
                if (_dbContext.FolderHashCods.Count() > 0)
                {
                    resoult = _dbContext.FolderHashCods.First();
                    resoult.value = hash;
                    if (_dbContext.FolderHashCods.Count() > 1)
                    {
                        var removelist = _dbContext.FolderHashCods.Where(el => el.Id != resoult.Id).ToList();
                        if (removelist.Count > 0)
                        {
                            foreach (var item in removelist)
                            {
                                _dbContext.FolderHashCods.Remove(item);
                                Save();
                            }
                        }
                    }
                }
                if (_dbContext.FolderHashCods.Count() == 0)
                {
                    _dbContext.FolderHashCods.Add(new FolderHashCod() { value = hash });
                    Save();
                    resoult = _dbContext.FolderHashCods.First();
                }
            }
            return resoult;
        }

        public void Update(FolderHashCod folderHashCod)
        {
            Update<FolderHashCod>(folderHashCod);
        }

        public bool DelitFolderHashCod()
        {
            var folderHashCod = GetFolderHashCod();
            if (folderHashCod != null)
            {
                lock (_lockDb)
                {
                    _dbContext.FolderHashCods.Remove(folderHashCod);
                    Save();
                    return true;
                }
            }
            return false;
        }
    }
}
