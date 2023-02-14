using ASP_DZ_6_Books.Data;
using ASP_DZ_6_Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksContext _booksContext;

        public HomeController(ILogger<HomeController> logger, BooksContext context)
        {
            _logger = logger;
            _booksContext = context;

        }

        public async Task<ActionResult<IEnumerable<Book>>> IndexAsync()
        {
            IQueryable<Book> books = _booksContext.Books;

            return View(await books.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
