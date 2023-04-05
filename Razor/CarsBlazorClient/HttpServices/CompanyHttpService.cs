using CarsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public class CompanyHttpService : ICompanyHttpService
    {
        private readonly HttpClient _typedHttpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public CompanyHttpService(HttpClient typedHttpClient)
        {
            _typedHttpClient = typedHttpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase

            };
        }
        public async Task<List<CompanyDetailsDTO>> GetCompaiesDetails()
        {
            var response = await _typedHttpClient.GetFromJsonAsync<IEnumerable<CompanyDetailsDTO>>("Companies/GetCompaniesDetails");
            return response.ToList();
        }

        public async Task<List<CompanyDTO>> GetCompanies()
        {
            var response = await _typedHttpClient.GetFromJsonAsync<IEnumerable<CompanyDTO>>("Companies/GetCompanies");
            return response.ToList();
        }
    }
}
