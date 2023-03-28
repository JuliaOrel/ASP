using CountriesCities.Data;
using CountriesCities.Data.Entities;
using CountriesCities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Repositories
{
    public class CityReposotory: ICityRepository
    {
        private readonly CountriesCitiesContext _context;
        public CityReposotory(CountriesCitiesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            IQueryable<City> entities = _context.Cities
                .Where(c=>c.IsDeleted==false);
            
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesDetails()
        {
            IQueryable<City> entities = _context.Cities
                .Where(c => c.IsDeleted == false);
            foreach (City city in entities)
            {
                await _context.Countries
                    .Where(c => c.Id == city.CountryId && c.IsDeleted == false)
                    .LoadAsync();
            }
            return await entities.ToListAsync();
        }

        public async Task<City> GetCity(int id)
        {
            City entity = await _context.Cities
                .FirstOrDefaultAsync(c=>c.Id==id && c.IsDeleted==false);
            return entity;
        }

        public async Task<City> GetCityDetails(int id)
        {
            City entity = await _context.Cities
                .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            if(entity is null)
            {
                return null;
            }
            await _context.Countries
                .Where(c => c.Id == entity.CountryId && c.IsDeleted == false)
                .LoadAsync();
            return entity;
        }
    }
}
