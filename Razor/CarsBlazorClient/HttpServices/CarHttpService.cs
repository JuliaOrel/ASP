using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsBlazorClient.HttpServices
{
    public class CarHttpService : ICarHttpService
    {
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

        public Task<List<CarDTO>> GetCars()
        {
            throw new NotImplementedException();
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
