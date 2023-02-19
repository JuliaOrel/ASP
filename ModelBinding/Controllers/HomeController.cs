using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelBinding.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static List<Trail> _trails = new List<Trail>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<TrailVM> trails = _trails.Select(t =>
               {
                   return new TrailVM
                   {
                       Name = t.Name,
                       TrailLength = t.TrailLength
                   };
               }).ToList();
            return View(trails);

        }


    [HttpGet]
    public IActionResult CreateTrail()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CreateTrail(TrailVM trailVM)
    {
            Trail trail = new Trail
            {
                Name = trailVM.Name,
                TrailLength = trailVM.TrailLength
            };
        //trail.Id = Guid.NewGuid().ToString();
        _trails.Add(trail);
        return RedirectToAction(nameof(Index));
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
