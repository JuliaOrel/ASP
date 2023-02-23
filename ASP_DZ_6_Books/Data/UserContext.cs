using ASP_DZ_6_Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
: base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users => Set<User>();
    }
}
