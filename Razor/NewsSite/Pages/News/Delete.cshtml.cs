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
    public class DeleteModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;

        public DeleteModel(NewsSite.Data.NewsBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsOne = await _context.NewsOne.FindAsync(id);

            if (NewsOne != null)
            {
                _context.NewsOne.Remove(NewsOne);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
