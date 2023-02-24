using ASP_DZ_6_Books.Data;
using ASP_DZ_6_Books.Models;
using ASP_DZ_6_Books.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Controllers
{
    public class AccountController : Controller
    {
        private readonly BooksContext _context;
        public AccountController(BooksContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {

            if (ModelState.IsValid == true)
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user is not null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect email or password");
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid == true)
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user is null)
                {
                    User newUser = new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    };
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    await Authenticate(model.Email);
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("", "Incorrect email or password");
            }
            return View(model);
        }

        private async Task Authenticate(string email)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
            };
            ClaimsIdentity identity = new ClaimsIdentity(
                claims: claims,
                authenticationType: "ApplicationCookie",
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(
                scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                principal: new ClaimsPrincipal(identity)
                );

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
