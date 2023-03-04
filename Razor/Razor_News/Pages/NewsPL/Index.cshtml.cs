using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_News.Data;
using Razor_News.Models;

namespace Razor_News.Pages.NewsPL
{
    public class IndexModel : PageModel
    {
        private readonly Razor_News.Data.Razor_NewsContext _context;

        public IndexModel(Razor_News.Data.Razor_NewsContext context)
        {
            _context = context;
        }

        public IList<News> NewsL { get;set; }

        public async Task OnGetAsync()
        {
            News = await _context.NewsList.ToListAsync();
        }
    }
}
