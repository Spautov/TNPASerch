using DAL;
using System;
using System.Collections.Generic;

namespace Repository
{
    interface IRepository : IDisposable
    {
        IEnumerable<Tnpa> GetTnpaList();
        IEnumerable<TnpaType> GetTnpaTypeList();
        Tnpa GetTnpa(int id);
        TnpaType GetTnpaType(int id);
        void Create(Tnpa item);
        void Create(TnpaType item);
        void Update(Tnpa item);
        void Update(TnpaType item);
        void DeleteTnpa(int id);
        void DeleteTnpaType(int id);
        void Save();
    }
}
