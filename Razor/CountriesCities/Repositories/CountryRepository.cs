using CountriesCities.Data;
using CountriesCities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        private readonly CountriesCitiesContext _context;
        public CountryRepository(CountriesCitiesContext context)
        {
            _context = context;
        }
    }
}
