using Medicines.Data.Entities;
using Medicines.Models.ViewModels;
using Medicines.Services.EmailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IEmailService _emailService;
        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email, // в имейле
                    UserName = model.Email // и имени будет ...имейл
                };

                // добавляем пользователя
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // генерация токена для пользователя
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    string callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new
                        {
                            userId = user.Id,
                            code = code
                        },
                        protocol: HttpContext.Request.Scheme)!;

                    await _emailService.SendEmailAsync(
                        nameof(Medicines),
                        model.Email,
                        "Confirm your account",
                        $"For confirm registration <a href='{callbackUrl}'>follow the link</a>");

                    return View("ConfirmRegistration");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("ConfirmedAccount", "Account");
            }
            else
            {
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult ConfirmedAccount()
        {
            return View("ConfirmedAccount");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginVM { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                // или же FindByEmailAsync(model.Email)


                if (user != null)
                {
                    // проверяем, подтвержден ли email

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You are not confirmed your Email");
                        return View(model);
                    }
                }

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager
                    .PasswordSignInAsync(
                    userName: model.Email,
                    password: model.Password,
                    isPersistent: model.RememberMe, // Флаг, указывающий, должен ли файл cookie для входа сохраняться после закрытия браузера.
                    lockoutOnFailure: false); // Флаг, указывающий, следует ли блокировать учетную запись пользователя в случае сбоя входа.
                if (signInResult.Succeeded)
                {
                    //избегагаем перенаправления на нежелательные сайты
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
    Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login and (or) password");
                }
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}
