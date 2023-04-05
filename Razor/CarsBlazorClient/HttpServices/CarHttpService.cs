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
        public async Task<CarDTO> DelCar(int id)
        {
            HttpClient httpClient = CreateNamedHttpClient();
            HttpResponseMessage response = await httpClient.DeleteAsync($"Cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                string stringResponse = await response.Content.ReadAsStringAsync();
                CarDTO result = JsonSerializer.Deserialize<CarDTO>(stringResponse, _jsonSerializerOptions);
                return result;
            }
            return null;
        }

        public async Task<CarDTO> GetCar(int id)
        {
            HttpClient httpClient = CreateNamedHttpClient();
            var response = await httpClient.GetFromJsonAsync<CarDTO>($"Cars/GetCar/{id}");
            return response;
        }

        public async Task<CarDetailsDTO> GetCarDetails(int id)
        {
            HttpClient httpClient = CreateNamedHttpClient();
            var response = await httpClient.GetFromJsonAsync<CarDetailsDTO>($"Cars/GetCarDetails/{id}");
            return response;
        }

        public async Task<List<CarDTO>> GetCars()
        {
            HttpClient httpClient = CreateNamedHttpClient();
            //HttpResponseMessage response = await httpClient
            //    .GetAsync("/GetCars");

            //string content = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode == false)
            //{
            //    throw new ApplicationException(content);
            //}
            //var cars = JsonSerializer.Deserialize<List<CarDTO >> (content, _jsonSerializerOptions);
            //return cars;
            var response = await httpClient.GetFromJsonAsync<IEnumerable<CarDTO>>("Cars/GetCars");
            return response.ToList();
        }

        private HttpClient CreateNamedHttpClient()
        {
            return _httpClientFactory.CreateClient("CarsHttpClient");
        }

        public async Task<List<CarDetailsDTO>> GetCarsDetails()
        {
            HttpClient httpClient = CreateNamedHttpClient();
            var response = await httpClient.GetFromJsonAsync<IEnumerable<CarDetailsDTO>>("Cars/GetCarsDetails");
            return response.ToList();

        }

        public async Task<CarDTO> PostCar(CarDTO car)
        {
            HttpClient httpClient = CreateNamedHttpClient();
            HttpResponseMessage response = await httpClient.PostAsJsonAsync<CarDTO>("Cars", car);
            if(response.IsSuccessStatusCode)
            {
                string stringResponse = await response.Content.ReadAsStringAsync();
                CarDTO result = JsonSerializer.Deserialize<CarDTO>(stringResponse, _jsonSerializerOptions);
                return result;
            }
            return null;
        }

        public async Task<CarDTO> PutCar(CarDTO car)
        {
            HttpClient httpClient = CreateNamedHttpClient();
            HttpResponseMessage response = await httpClient.PutAsJsonAsync<CarDTO>($"Cars/{car.Id}", car);
            if (response.IsSuccessStatusCode)
            {
                string stringResponse = await response.Content.ReadAsStringAsync();
                CarDTO result = JsonSerializer.Deserialize<CarDTO>(stringResponse, _jsonSerializerOptions);
                return result;
            }
            return null;
        }
    }
}
