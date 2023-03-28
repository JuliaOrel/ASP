using AutoMapper;
using CountriesCities.Data.Entities;
using CountriesCities.Interfaces;
using CountriesCitiesShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Services
{
    public class CityService: ICityService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        public CityService(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityDTO>> GetCities()
        {
            IEnumerable<City> entities = await _cityRepository.GetCities();
            IEnumerable<CityDTO> result = _mapper.Map<IEnumerable<CityDTO>>(entities);
            return result;
        }

        public async Task<IEnumerable<CityDetailsDTO>> GetCitiesDetails()
        {
            IEnumerable<City> entities = await _cityRepository.GetCitiesDetails();
            IEnumerable<CityDetailsDTO> result = _mapper.Map<IEnumerable<CityDetailsDTO>>(entities);
            return result;

        }

        public async Task<CityDTO> GetCity(int id)
        {
            City entity = await _cityRepository.GetCity(id);
            if(entity is null)
            {
                return null;
            }
            CityDTO result = _mapper.Map<CityDTO>(entity);
            return result;
        }

        public async Task<CityDetailsDTO> GetCityDetails(int id)
        {
            City entity = await _cityRepository.GetCityDetails(id);
            if (entity is null)
            {
                return null;
            }
            CityDetailsDTO result = _mapper.Map<CityDetailsDTO>(entity);
            return result;
        }
    }
}
