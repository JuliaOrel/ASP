using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var rand = new Random();
            char letter = Convert.ToChar(rand.Next(65, 90));
            ViewData["someKey"] = letter;
            return View();
        }
        public IActionResult Restaurants()
        {
            return View();
        }

        public IActionResult Countries()
        {
            return View();
        }





    }
}
