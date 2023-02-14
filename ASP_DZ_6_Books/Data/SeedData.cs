using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Data
{
    public static class SeedData
    {
        public static async Task Initialize(BooksContext context)
        {
            if(context.Books.Any()==false)
            {
                context.Books.AddRange(
                    new Models.Book
                    {
                        NameBook="Catch and Kill",
                        FIOAuthor= "Ronan Farrow",
                        Genre="True Crime",
                        Publisher= "Little, Brown and Company",
                        YearIssue=2019
                    },
                     new Models.Book
                     {
                         NameBook = "Dare to lead",
                         FIOAuthor = "B. Brown",
                         Genre = "Self-help",
                         Publisher = "VERMILION",
                         YearIssue = 2018
                     }
                    );
                await context.SaveChangesAsync();
            }
        }
    }
}
