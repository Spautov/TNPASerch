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

        private async Task<string> GetData(string url)
        {
            using (var client = new HttpClient(_clientHandler, false))
            {
                var result = await client.GetAsync(url);
                var content = await result.Content.ReadAsStringAsync();
                return content;
            }
        }

        public async Task<List<DocumentBriefInfo>> GetDataAsync(string typeName , string number)
        {
            try
            {
                var content = await GetData($"https://tnpa.by/api/tnpadocs?page=1&per-page=100&sort=b.KL&SearchParam={typeName}%20{number}&lang=ru&stateID=-1&onlyActive=null&depID=0");
                var data = JsonConvert.DeserializeObject<List<DocumentBriefInfo>>(content);
                return data;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<List<DocumentfInfo>> GetDataByIdGlobalAsync(int idGlobal, int rn)
        {
            try
            {
                var content = await GetData($"https://tnpa.by/api/basicdatas?IDGLOBAL={idGlobal}&RN={rn}&lng=ru");
                var data = JsonConvert.DeserializeObject<List<DocumentfInfo>>(content);
                return data;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
