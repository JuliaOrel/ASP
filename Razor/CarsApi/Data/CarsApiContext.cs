using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarsShared.Models;

namespace CarsApi.Data
{
    public class CarsApiContext : DbContext
    {
        public CarsApiContext (DbContextOptions<CarsApiContext> options)
            : base(options)
        {
        }

        public DbSet<CarsShared.Models.Car> Cars { get; set; }
        public DbSet<CarsShared.Models.Company> Companies { get; set; }
    }
}
