using ProductStore2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore2.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ProductContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Products.Any() == false)
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "TV Vision X3",
                        Company = "Xiaomi",
                        Price = 1200
                    },
                    new Product
                    {
                        Name = "Mate 40 pro",
                        Company = "huawei",
                        Price = 1000
                    },
                    new Product
                    {
                        Name = "Samsung galaxy x22",
                        Company = "Samsung",
                        Price = 2000
                    });
                await context.SaveChangesAsync();
            }
        }
    }
}
