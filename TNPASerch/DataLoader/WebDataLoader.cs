using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataLoader
{
    public class WebDataLoader
    {
        private readonly HttpMessageHandler _clientHandler;
        public WebDataLoader()
        {
            _clientHandler = new HttpClientHandler();
        }

        public async Task<List<TechnicalDocument>> GetDataAsync()
        {
            try
            {
                var content = await GetData(@"https://tnpa.by/api/tnpadocs?page=1&per-page=100&sort=b.KL&SearchParam=%D0%A1%D0%A2%D0%91%201033&lang=ru&stateID=-1&onlyActive=null&depID=0");
                var data = JsonConvert.DeserializeObject<List<TechnicalDocument>>(content);
                return data;
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        private async Task<string> GetData(string url)
        {
            using (var client = new HttpClient(_clientHandler, false))
            {
                var result = await client.GetAsync(url);
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
