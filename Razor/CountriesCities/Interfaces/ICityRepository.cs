using CountriesCities.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<City>> GetCitiesDetails();
        Task<City> GetCity(int id);
        Task<City> GetCityDetails(int id);
    }
}
