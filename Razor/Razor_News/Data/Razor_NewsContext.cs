using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Razor_News.Models;

namespace Razor_News.Data
{
    public class Razor_NewsContext : DbContext
    {
        public Razor_NewsContext (DbContextOptions<Razor_NewsContext> options)
            : base(options)
        {
        }

        public DbSet<Razor_News.Models.News> NewsList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().ToTable("News");
           
        }

    }
}
