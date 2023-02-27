using Medicines.Data.Entities;
using Medicines.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Components
{
    public class LastViewedMedicinesViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<Medicine> sessionPosts = new List<Medicine>();

            foreach (string key
                in HttpContext.Session.Keys
                    .Where(k => k.Contains("LastViewedPosts")))
            {
                sessionPosts.Add(HttpContext.Session.Get<Medicine>(key)!);
            }

            return View(sessionPosts);
        }

    }
}
