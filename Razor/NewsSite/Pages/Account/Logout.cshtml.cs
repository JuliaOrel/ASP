using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewsSite.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            var authProps = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authProps);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
