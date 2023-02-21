using CookieAuthExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieAuthExample.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext>options):base(options)
        { //Database.EnsureDeleted();
          Database.EnsureCreated(); }
        public DbSet<User> Users => Set<User>();
    }
}
