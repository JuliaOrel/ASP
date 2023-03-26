using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace CarsApi.Data
{
    public class CarsApiContext : DbContext
    {
        public CarsApiContext (DbContextOptions<CarsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Shared.Models.Car> Cars { get; set; }
    }
}
