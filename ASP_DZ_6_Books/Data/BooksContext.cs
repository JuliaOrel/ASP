using ASP_DZ_6_Books.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Data
{
    public class BooksContext: DbContext
    {
        public BooksContext(DbContextOptions<BooksContext>options)
            : base(options) { }
        public DbSet<Book> Books => Set<Book>();


    }
}
