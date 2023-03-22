using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Auth0.Pages.Account.UserProfile
{
    public class UserProfileModel : PageModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public void OnGet()
        {
            Name = User.Identity.Name;
            Email = User.FindFirst(e => e.Type == ClaimTypes.Email)?.Value;
            Image = User.FindFirst(c => c.Type == "picture")?.Value;
        }
    }
}
