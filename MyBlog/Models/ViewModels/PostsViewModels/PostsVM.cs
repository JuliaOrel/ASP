using MyBlog.Data.Entitties;
using MyBlog.Models.ViewModels.NavigationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models.ViewModels
{
    public class PostsVM
    {

        public IEnumerable<Post> Posts { get; set; }
        //public IEnumerable<Category> Categories { get; set; } 
        //public int CategoryId { get; set; }
        public FilterVM FilterVM { get; set; }
        public SortVM SortVM { get; set; }
        public PageVM PageVM { get; set; }
        public PostsVM(IEnumerable<Post>posts, FilterVM filterVM, SortVM sortVM, PageVM pageVM)
        {
            Posts = posts;
            FilterVM = filterVM;
            SortVM = sortVM;
            PageVM = pageVM;

        }

    }
}
