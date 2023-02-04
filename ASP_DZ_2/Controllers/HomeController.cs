using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP_DZ_2.Models;

namespace ASP_DZ_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<Movie> movies;
        List<Session> sessions;
      
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            movies = new List<Movie>();
            movies.Add(movie);
            movies.Add(movie2);
            Session session = new Session { TimeSession = "15:45", MovieId = 1, Movie=movie };
            sessions = new List<Session>();
            sessions.Add(session);
           
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Movies()
        {
            
            if (movies.Count > 0)
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    string data = $"{movies[i].SessionList}";
                    ViewData["someKey"] = $"{movies[i].Name} {movies[i].SessionList}";
                }
            }
            
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
