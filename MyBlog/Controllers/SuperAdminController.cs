using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class SuperAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
