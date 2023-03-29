using AutoMapper;
using CarsApi.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Services
{
    public class CarService: ICarService
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public CarService(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task<CarDTO> GetCar(int id)
        {
            Car entity = await _carRepository.GetCar(id);
            if(entity is null)
            {
                return null;
            }
            CarDTO car = _mapper.Map<CarDTO>(entity);
            return car;
        }

        public async Task<CarDetailsDTO> GetCarDetails(int id)
        {
            Car entity = await _carRepository.GetCarDetails(id);
            if (entity is null)
            {
                return null;
            }
            CarDetailsDTO car = _mapper.Map<CarDetailsDTO>(entity);
            return car;
        }

        public async Task<IEnumerable<CarDTO>> GetCars()
        {
            IEnumerable<Car> entities = await _carRepository.GetCars();
            IEnumerable<CarDTO> cars =
                _mapper.Map<IEnumerable<CarDTO>>(entities);
            return cars;
        }

        public async Task<IEnumerable<CarDetailsDTO>> GetCarsDetails()
        {
            IEnumerable<Car> entities = await _carRepository.GetCarsDetails();
            IEnumerable<CarDetailsDTO> cars =
                _mapper.Map<IEnumerable<CarDetailsDTO>>(entities);
            return cars;
        }
    }
}
