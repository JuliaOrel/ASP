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
        private readonly MobileContext _mobileContext;

        public HomeController(ILogger<HomeController> logger, MobileContext context)
        {
            _logger = logger;
            _mobileContext = context;
        }

        public async Task<ActionResult<IEnumerable<Movie>>> IndexAsync()
        {

            IQueryable<Movie> movies = _mobileContext.Movies;
            IQueryable<Session> sessions = _mobileContext.Sessions;
            return View(await _mobileContext.Movies.Include(m => m.SessionList).ToListAsync()

            );
        }

        //public async Task<IActionResult> Index()
        //{ 

        //    var movies = await _mobileContext.Movies
        //        .Include(m => m.SessionList)
        //        .ToListAsync();

        //    return View(movies);
        //}
        [HttpGet]
        public IActionResult MakeOrder(int? id)
        {
            if(id is null)
            {
                return NotFound();
            }
            ViewBag.MovieId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(Session session)
        {
            _mobileContext.Sessions.Add(session);
            await _mobileContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
