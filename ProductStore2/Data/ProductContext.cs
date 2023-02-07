using Microsoft.EntityFrameworkCore;
using ProductStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Data
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions<ProductContext>options)
        :base(options){}
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
    }
}
