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
    }
}
