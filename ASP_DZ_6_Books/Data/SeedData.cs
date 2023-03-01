using ASP_DZ_6_Books.Models;
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
        public static async Task Initialize(IServiceProvider serviceProvider,
                IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            DbContextOptions<BooksContext> options =
                 serviceProvider.GetRequiredService<DbContextOptions<BooksContext>>();
            using (BooksContext context = new BooksContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                if (context.Books.Any() == false)
                {
                    byte[] bookImage1 = File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\catch.jpg");
                    byte[] bookImage2 = File.ReadAllBytes($"{webHostEnvironment.WebRootPath}\\images\\dare.jpg");

                    Book book1 = new Book()
                    {
                        NameBook = "Catch and Kill",
                        FIOAuthor = "Ronan Farrow",
                        Genre = "True Crime",
                        Publisher = "Little, Brown and Company",
                        YearIssue = 2019,
                        Image = bookImage1,

                    };

                    Book book2 = new Book()
                    {
                        NameBook = "Dare to lead",
                        FIOAuthor = "B. Brown",
                        Genre = "Self-help",
                        Publisher = "VERMILION",
                        YearIssue = 2018,
                        Image = bookImage2
                    };
                    List<Tag> tagList1 = new List<Tag>
                {
                    new Tag {Name="#catch", Book=book1},
                    new Tag {Name="#kill", Book=book1}

                };
                    List<Tag> tagList2 = new List<Tag>
                {
                    new Tag {Name="#dare", Book=book2},
                    new Tag {Name="#lead", Book=book2}

                };
                    book1.Tags = tagList1;
                    book2.Tags = tagList2;
                    List<Book> books = new List<Book>
                {
                    book1,
                    book2
                };

                    context.Books.AddRange(books);
                    context.Tags.AddRange(tagList1);
                    context.Tags.AddRange(tagList2);
                    await context.SaveChangesAsync();
                }
            }


        }
    }
}
