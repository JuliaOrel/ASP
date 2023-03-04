using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsSite.Models;

namespace NewsSite.Data
{
    public class NewsBlogContext : DbContext
    {
        public NewsBlogContext (DbContextOptions<NewsBlogContext> options)
            : base(options)
        {
        }

        public DbSet<NewsSite.Models.NewsOne> NewsOne { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsOne>().ToTable("NewsOne");

        }
    }
}
