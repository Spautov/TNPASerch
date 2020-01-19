using DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Tnpa> GetTnpaList();
        ValueTask<IEnumerable<Tnpa>> GetTnpaListAsunc();
        IEnumerable<TnpaType> GetTnpaTypeList();
        ValueTask<IEnumerable<TnpaType>> GetTnpaTypeListAsunc();
        Tnpa GetTnpa(int id);
        TnpaType GetTnpaType(int id);
        void Create(Tnpa item);
        void Create(TnpaType item);
        void Update(Tnpa item);
        void Update(TnpaType item);
        void DeleteTnpa(int id);
        void DeleteTnpaType(int id);
        TnpaType FindTnpaTypeByName(string name);
        TnpaType FindTnpaTypeById(int id);
        Tnpa FindTnpaByName(string name);
        Tnpa FindTnpaByNumber(string name);
        Tnpa FindTnpaById(int id);
        void Save();
    }
}
