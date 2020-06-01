using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using movies.Interfaces;
using Newtonsoft.Json;

namespace movies.Services
{
    public class SimpleRequestService : ISimpleRequestService
    {
        public async Task<T> GetSimpleAsync<T>(string requestParam)
        {
                return await this.downloadAsync<T>(requestParam);
        }

        private async Task<T> downloadAsync<T>(string requestParam)
        {
            using (var client = new HttpClient())
            {
                var url = AppConfig.ConfigConstants.BaseApiURL + requestParam;
                var response = await client.GetAsync(url).ConfigureAwait(false);
#if DEBUG
                Debug.WriteLine($"StatusCode {response.StatusCode} / ResponseMessage: {response.RequestMessage}");
#endif
                if (!response.IsSuccessStatusCode)
                {
                    var exception = new Exception($"Api Error: {response.RequestMessage}");
                }

                var json = await response.Content.ReadAsStringAsync();
#if DEBUG
                Debug.WriteLine($"RESULT: {json.Substring(0, json.Length > 200 ? 200 : json.Length)}");
#endif
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}