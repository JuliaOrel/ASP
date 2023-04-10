using ManageLots.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManageLots.Client.Services
{
    public class LotHttpService
    {
        private readonly HttpClient _typedHttpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public LotHttpService(HttpClient typedHttpClient)
        {
            _typedHttpClient = typedHttpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented=true
            };
        }

        public async Task<List<LotGetModel>>GetLots()
        {
            IEnumerable<LotGetModel> lots = await _typedHttpClient
                .GetFromJsonAsync<IEnumerable<LotGetModel>>("", _jsonSerializerOptions);
            if(lots is null)
            {
                return new List<LotGetModel>();
            }
            return lots.ToList();
        }

        public async Task<ApiResponseModel>BuyLot(string messageId, string popReceipt)
        {
            HttpResponseMessage httpResponse = await _typedHttpClient
                .DeleteAsync($"/{messageId}/{popReceipt}");
            ApiResponseModel result;
            //if(httpResponse.IsSuccessStatusCode)
            //{
                string stringResponse = await httpResponse.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<ApiResponseModel>(stringResponse, _jsonSerializerOptions);
                return result;
            //    
        }

        public async Task<LotGetModel>AddLot(LotAddModel lotAddModel)
        {
            HttpResponseMessage httpResponse = await _typedHttpClient
                .PostAsJsonAsync<LotAddModel>("", lotAddModel);

            if(httpResponse.IsSuccessStatusCode)
            {
                string stringResponse = await httpResponse.Content.ReadAsStringAsync();
                LotGetModel result=JsonSerializer.Deserialize<LotGetModel>(stringResponse, _jsonSerializerOptions);

                return result;
            }
            return null;
        }
    }
}
