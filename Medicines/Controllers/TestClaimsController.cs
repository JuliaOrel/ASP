using Medicines.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Controllers
{
    public class TestClaimsController: Controller
    {
        [Authorize(Policy = MyPolicies.SuperAdminAccessOnly)]
        public IActionResult SuperAdmin()
        {
            return Content(User.Identity.Name);
        }

        [Authorize(Policy = MyPolicies.AdminAndAboveAccess)]
        public IActionResult Admin()
        {
            return Content(User.Identity.Name);
        }

        [Authorize(Policy = MyPolicies.PostsWriterAndAboveAccess)]
        public IActionResult PostsWriter()
        {
            return Content(User.Identity.Name);
        }
    }
}
