using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Entitties;
using MyBlog.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Components
{
    public class LastViewedPostsViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<Post> sessionPosts = new List<Post>();

            foreach (string key
                in HttpContext.Session.Keys
                    .Where(k => k.Contains("LastViewedPosts")))
            {
                sessionPosts.Add(HttpContext.Session.Get<Post>(key)!);
            }

            return View(sessionPosts);
        }

    }
}
