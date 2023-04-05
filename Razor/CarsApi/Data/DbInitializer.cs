using Microsoft.Extensions.DependencyInjection;
using CarsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsApi.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            CarsApiContext _context = services.GetRequiredService<CarsApiContext>();
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();

            if (_context.Cars.Any())
            {
                return;
            }
            Company company1 = new Company
            {
                Name = "BMW"
            };
            Company company2 = new Company
            {
                Name = "Mazda"
            };
            Company company3 = new Company
            {
                Name = "Volkswagen"
            };
            Car car1 = new()
            {
                Model = "Countryman",
                Brand = "MINI Cooper",
                Color = "Chilli Red",
                Price = 1479151,
                YearIssue = 2020,
                Company = company1
            };
            Car car2 = new()
            {
                Model = "CX-30",
                Brand = "Mazda",
                Color = "Magma Red",
                Price = 1108880,
                YearIssue = 2020,
                Company = company2
            };
            Car car3 = new()
            {
                Model = "T-Roc",
                Brand = "Volkswagen",
                Color = "Diamond Metallic",
                Price = 1212412,
                YearIssue = 2021,
                Company = company3
            };
            List<Car> cars = new List<Car>() { car1, car2, car3 };
            await _context.Cars.AddRangeAsync(cars);
            await _context.SaveChangesAsync();
        }
    }
}
