using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor1.Pages
{
    public class BlogModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int PostsCount { get; set; }
        public void OnGet()
        {
            Message = "Enter the data";
        }

        public void OnPost(/*string name, int postsCount*/)
        {
            //Message = "Name " + name + "; postsCount " + postsCount;
            Message = "Name " + Name + ";postsCount " + PostsCount;
        }
    }
}
