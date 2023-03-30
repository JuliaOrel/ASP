using CountriesCitiesShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetCities();
        Task<IEnumerable<CityDetailsDTO>> GetCitiesDetails();
        Task<CityDTO> GetCity(int id);
        Task<CityDetailsDTO> GetCityDetails(int id);

        
    }
}
