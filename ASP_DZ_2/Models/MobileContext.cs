using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2.Models
{
    public class MobileContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public MobileContext(DbContextOptions<MobileContext>options)
            :base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
