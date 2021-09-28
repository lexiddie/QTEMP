using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QTEMP.Models;

namespace QTEMP.Providers
{
    public class ApiProvider : IApiProvider
    {
        private const string Url = "your-url-end-point";
        private const string Key = "x-api-key";
        private const string Value = "your-api-key-value";
        
        public async Task<Response> PostHealth(Temperature temperature)
        {
            Response data;
            using (var httpClient = new HttpClient())
            {
                var jsonInString = JsonConvert.SerializeObject(temperature);
                httpClient.BaseAddress = new Uri(Url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add(Key, Value);
                using var response = await httpClient.PostAsync( "v1/healthLog/submit", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var res = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<Response>(res);
                data.IsSuccess = response.IsSuccessStatusCode;
            }
            return data;
        }
    }
}