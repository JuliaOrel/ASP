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
    }
}
