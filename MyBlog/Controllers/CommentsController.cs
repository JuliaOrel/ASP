using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.Data;
using MyBlog.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<CommentsController> _logger;
        private readonly UserManager<User> _userManager;

        public IActionResult Index()
        {
            return View();
        }
    }
}
