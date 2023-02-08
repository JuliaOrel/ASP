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

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            //ViewBag.MovieId = id;
           // List<Session> list = new List<Session>();
            Movie movie = await _moviesContext.Movies
                .Include(m => m.Sessions)
                .FirstOrDefaultAsync(m => m.ID == id);
                //Find(id);
            //for (int i = 0; i < _moviesContext.Sessions.Count(); i++)
            //{
            //    Session ses = _moviesContext.Sessions.Find(movie.ID);
            //    list.Add(ses);
            //}
               
            
            //Session session = _moviesContext.Sessions.Find(movie.ID);
            //movie.Sessions = (ICollection<Session>)session;

            return View(movie);


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
