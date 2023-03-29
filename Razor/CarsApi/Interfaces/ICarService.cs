using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDTO>> GetCars();
        Task<IEnumerable<CarDetailsDTO>> GetCarsDetails();
    }
}
