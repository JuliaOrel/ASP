using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor1.Models;

namespace Razor1.Pages
{
    public class PostModel : PageModel
    {
        List<Post> _posts;
        public List<Post> DisplayedPosts { get; set; }
        public PostModel()
        {
            _posts = new List<Post>
            {
                new Post{Name="Post1", Rating=3},
                new Post{Name="Post1", Rating=5},
                new Post{Name="Post1", Rating=2},
                new Post{Name="Post1", Rating=3},
            };
            
        }

        public void OnGet()
        {
            DisplayedPosts = _posts;
        }

        //?handler=ByName&name=Post1
        //post/ByRating?rating=3 - if we determined "{handler}"
        public void OnGetByName(string name)
        {
            DisplayedPosts = _posts.Where(x => x.Name.Contains(name)).ToList();
        }

        public void OnGetByRating(int rating)
        {
            DisplayedPosts = _posts.Where(x => x.Rating==rating).ToList();
        }
    }
}
