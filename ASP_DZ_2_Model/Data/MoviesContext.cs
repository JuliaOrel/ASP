using ASP_DZ_2_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Data
{
    public class MoviesContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions => Set<Session>();
        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
