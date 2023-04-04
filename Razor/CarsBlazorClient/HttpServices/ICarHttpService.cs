using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public interface ICarHttpService
    {
        Task<List<CarDTO>> GetCars();
        Task<List<CarDetailsDTO>> GetCarsDetails();
        Task<CarDTO> GetCar(int id);
        Task<CarDetailsDTO> GetCarDetails(int id);
        Task<CarDTO> PostCar(CarDTO car);
        Task<CarDTO> PutCar(CarDTO car);
        Task<CarDTO> DelCar(int id);
    }
}
