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
        [BindProperty]
        public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsOne = await _context.NewsOne.Include(f=>f.Comments).FirstOrDefaultAsync(m => m.ID == id);

            if (NewsOne == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddComment()
        {
           
            await _context.Comment.AddAsync(Comment);
            await _context.SaveChangesAsync();
            //Comment = await _context.Comment.FirstOrDefaultAsync(c => c.ID == id);

            return RedirectToPage("Details", new { id = Comment.NewsOneId });
        }
    }
}
