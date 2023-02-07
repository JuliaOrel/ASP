using ASP_DZ_2_Model.Data;
using ASP_DZ_2_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_2_Model.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoviesContext _moviesContext;

        public HomeController(ILogger<HomeController> logger, MoviesContext context)
        {
            _logger = logger;
            _moviesContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult<IEnumerable<Movie>>> MovieAsync()
        {
            return View(await _moviesContext.Movies
                .Include(m => m.Sessions)
                .ToListAsync()
            );
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            ViewBag.MovieId = id;
            return View();
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
