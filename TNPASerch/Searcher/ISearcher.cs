using DAL;
using System.Collections.Generic;

namespace Searcher
{
    public interface ISearcher
    {
        List<Tnpa> Serch();

        void Add(Tnpa tnpa);

        void Remove(Tnpa tnpa);
    }
}
