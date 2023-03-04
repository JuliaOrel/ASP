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
    public class DetailsModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;

        public DetailsModel(NewsSite.Data.NewsBlogContext context)
        {
            _context = context;
        }

        public NewsOne NewsOne { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsOne = await _context.NewsOne.FirstOrDefaultAsync(m => m.ID == id);

            if (NewsOne == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
