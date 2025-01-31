using DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Tnpa> GetTnpaList();
        Task<IEnumerable<Tnpa>> GetTnpaListAsunc();
        IEnumerable<TnpaType> GetTnpaTypeList();
        ValueTask<IEnumerable<TnpaType>> GetTnpaTypeListAsunc();
        Tnpa GetTnpa(int id);
        TnpaType GetTnpaType(int id);
        Task CreateAsync(Tnpa item);
        Task CreateAsync(TnpaType item);
        Task UpdateAsync(Tnpa item);
        Task UpdateAsync(TnpaType item);
        void DeleteTnpa(int id);
        void DeleteTnpaType(int id);
        TnpaType FindTnpaTypeByName(string name);
        TnpaType FindTnpaTypeById(int id);
        Tnpa FindTnpaByName(string name);
        IEnumerable<Tnpa> FindTnpaByNumber(string number);
        IEnumerable<Tnpa> SearchTnpaByNumber(string number);
        Tnpa FindTnpaById(int id);
        FolderHashCod GetFolderHashCod();
        FolderHashCod CreateFolderHashCod(int hash);
        void Update(FolderHashCod folderHashCod);
        bool DelitFolderHashCod();
        Task SaveAsync();
    }
}
