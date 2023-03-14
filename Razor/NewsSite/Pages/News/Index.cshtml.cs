using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models;

namespace NewsSite.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;

        public IndexModel(NewsSite.Data.NewsBlogContext context)
        {
            _context = context;
        }

        public IList<NewsOne> NewsOne { get;set; }

        public async Task OnGetAsync()
        {
            NewsOne = await _context.NewsOne.Include(c=>c.Comments).ToListAsync();
        }
      

    }
}
