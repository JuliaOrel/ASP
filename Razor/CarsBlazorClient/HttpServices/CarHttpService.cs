using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public class CarHttpService : ICarHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public CarHttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public Task<CarDTO> DelCar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CarDTO> GetCar(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CarDetailsDTO> GetCarDetails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarDTO>> GetCars()
        {
            HttpClient httpClient = CreateNamedHttpClient();
            HttpResponseMessage response = await httpClient
                .GetAsync("/GetCars");
            
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode == false)
            {
                throw new ApplicationException(content);
            }
            var cars = JsonSerializer.Deserialize<List<CarDTO >> (content, _jsonSerializerOptions);
            return cars;
        }

        private HttpClient CreateNamedHttpClient()
        {
            return _httpClientFactory.CreateClient("CarsHttpClient");
        }

        public Task<List<CarDetailsDTO>> GetCarsDetails()
        {
            throw new NotImplementedException();
        }

        public Task<CarDTO> PostCar(CarDTO car)
        {
            throw new NotImplementedException();
        }

        public Task<CarDTO> PutCar(CarDTO car)
        {
            throw new NotImplementedException();
        }
    }
}
