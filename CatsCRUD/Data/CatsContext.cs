using CatsCRUD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Data
{
    public class CatsContext:DbContext
    {
        public CatsContext(DbContextOptions<CatsContext> options)
           : base(options) { }
        public DbSet<Cat> Cats => Set<Cat>();
        public DbSet<Breed> Breeds => Set<Breed>();
    }
}
