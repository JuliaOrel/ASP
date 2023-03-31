using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCars();
        Task<IEnumerable<Car>> GetCarsDetails();
        Task<Car> GetCar(int id);
        Task<Car> GetCarDetails(int id);
        Task<Car> PostCar(Car entity);
        Task<Car> PutCar(Car entity);
        Task<Car> DeleteCar(Car entity);
        bool CityExists(int id);
    }
}
