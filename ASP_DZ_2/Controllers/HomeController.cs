using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP_DZ_2.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_DZ_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Movie> movies;
        List<Session> sessions;

        private readonly MobileContext _context;
      
        public HomeController(ILogger<HomeController> logger, MobileContext context)
        {
            _logger = logger;

            _context = context;
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            if (!_context.Movies.Any())
            {

                
                Movie movie = new Movie
                {
                    Name = "Men in black",
                    Director = "Barry Sonnenfeld",
                    Genre = "action",
                    Description = "ta-ta"
                };
                Movie movie2 = new Movie
                {
                    Name = "Venom",
                    Director = "Barry Sonnenfeld",
                    Genre = "action",
                    Description = "ta-ta"
                };

                Session session = new Session { TimeSession = "15:45",  Movie = movie };
                Session session2 = new Session { TimeSession = "16:45",  Movie = movie2 };

                List<Session> sessions1 = new List<Session>();
                List<Session> sessions2 = new List<Session>();

                sessions1.Add(session);
                sessions2.Add(session2);

                movie.SessionList = sessions1;
                movie2.SessionList = sessions2;

                movies = new List<Movie>();

                movies.Add(movie);
                movies.Add(movie2);

                _context.Movies.AddRange(movies);
                _context.Sessions.AddRange(sessions1);
                _context.Sessions.AddRange(sessions2);

                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public async Task<IActionResult> Movies()
        {

            //if (movies.Count > 0)
            //{
            //    for (int i = 0; i < movies.Count; i++)
            //    {
            //        string data = $"{movies[i].SessionList}";
            //        ViewData["someKey"] = $"{movies[i].Name} {movies[i].SessionList}";
            //    }
            //}

            var movies =  await _context.Movies
                .Include(m => m.SessionList)
                .ToListAsync();
            
            return View(movies);
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
