using CountriesCities.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            CountriesCitiesContext context = services.GetRequiredService<CountriesCitiesContext>();

            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (context.Cities.Any())
            {
                return;
            }

            Country ukraine = new Country
            {
                Name = "Ukraine",
                Code = 380,
            };

            Country unitedKingdom = new Country
            {
                Name = "United Kingdom",
                Code = 44,
            };

            Country usa = new Country
            {
                Name = "United States",
                Code = 1,
            };
            City krivyyRih = new City
            {
                Name = "Krivyy Rih",
                Population = 100500,
                AirPollutionDegree = "Unhealthy",
                Country = ukraine,
            };

            City kyiv = new City
            {
                Name = "Kyiv",
                Population = 1000_000,
                AirPollutionDegree = "Middle",
                Country = ukraine,
            };

            City lviv = new City
            {
                Name = "Lviv",
                Population = 5000_00,
                AirPollutionDegree = "Normal",
                Country = ukraine,
            };

            City london = new City
            {
                Name = "London",
                Population = 2000_00,
                AirPollutionDegree = "Moderate",
                Country = unitedKingdom,
            };

            City newYork = new City
            {
                Name = "New York",
                Population = 2000_000,
                AirPollutionDegree = "Danger",
                Country = usa,
            };
            City[] cities = new City[]
            {
                krivyyRih, kyiv, lviv, london, newYork
            };

            await context.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }
    }


}
