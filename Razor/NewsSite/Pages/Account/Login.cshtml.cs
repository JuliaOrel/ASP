using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewsSite.Pages.Account
{
    public class LoginModel : PageModel
    {
        public async Task OnGet(string returnUrl = "/")
        {

            var auth = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, auth);
        }
    }
}
