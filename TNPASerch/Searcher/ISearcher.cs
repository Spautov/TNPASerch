using DAL;
using System.Collections.Generic;

namespace Searcher
{
    public interface ISearcher
    {
        List<Tnpa> Serch(string request);

        void Add(Tnpa tnpa);

        void Remove(Tnpa tnpa);
    }
}
