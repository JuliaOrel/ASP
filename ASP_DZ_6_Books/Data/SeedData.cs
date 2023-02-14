using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration,
                IWebHostEnvironment webHostEnvironment)
        {
            DbContextOptions<BooksContext> options =
                 serviceProvider.GetRequiredService<DbContextOptions<BooksContext>>();
            using(BooksContext context=new BooksContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (context.Books.Any() == false)
                {
                    byte[] bookImage1 = File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\catch.jpg");
                    byte[] bookImage2 = File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\dare.jpg");
                    context.Books.AddRange(
                        new Models.Book
                        {
                            NameBook = "Catch and Kill",
                            FIOAuthor = "Ronan Farrow",
                            Genre = "True Crime",
                            Publisher = "Little, Brown and Company",
                            YearIssue = 2019,
                            Image=bookImage1
                        },
                         new Models.Book
                         {
                             NameBook = "Dare to lead",
                             FIOAuthor = "B. Brown",
                             Genre = "Self-help",
                             Publisher = "VERMILION",
                             YearIssue = 2018,
                             Image=bookImage2
                         }
                        );
                    await context.SaveChangesAsync();
                }
            }
            
            
        }
    }
}
