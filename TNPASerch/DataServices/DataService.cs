using DAL;
using DataLoader;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class DataService
    {
        public WebDataLoader WebDataLoader { get; }
        public IRepository Repository { get; }

        public DataService(WebDataLoader webDataLoader, IRepository repository)
        {
            WebDataLoader = webDataLoader;
            Repository = repository;
        }

        //GetTnpaListAsunc

        public async Task ChackTnpaListAsunc()
        {
            try
            {
                var data = await Repository.GetTnpaListAsunc();
                await ChackAndUpdateTnpaListAsunc(data);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private async Task ChackAndUpdateTnpaListAsunc(IEnumerable<Tnpa> tnpas)
        {
            foreach (var tnpa in tnpas) 
            {
                if (tnpa.GlobalId == null || tnpa.AdditionalId == null) 
                {
                    var res = await WebDataLoader.GetDataAsync(tnpa.Type.Name, tnpa.Number);
                    var apiTnpa = res.FirstOrDefault(el => el.Number.Equals($"{tnpa.Type.Name.ToUpper()} {tnpa.Number}-{tnpa.Year}"));

                    tnpa.GlobalId = apiTnpa.GlobalId;
                    tnpa.AdditionalId = apiTnpa.AdditionalId;
                    Repository.UpdateAsync(tnpa);
                }
            }
        }
    }
}
