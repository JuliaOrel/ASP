using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CountriesCities.Data.Entities;

namespace CountriesCities.Data
{
    public class CountriesCitiesContext : DbContext
    {
        public CountriesCitiesContext (DbContextOptions<CountriesCitiesContext> options)
            : base(options)
        {
        }

        public DbSet<CountriesCities.Data.Entities.City> Cities { get; set; }
        public DbSet<CountriesCities.Data.Entities.Country> Countries { get; set; }
    }
}
