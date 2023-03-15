using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsSite.Data;
using NewsSite.Models;

namespace NewsSite.Pages.News
{
    public class EditModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;

        public EditModel(NewsSite.Data.NewsBlogContext context)
        {
            _context = context;
        }

        [BindProperty]
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
        public async Task<IActionResult> OnPostDelComment(int? id)
        {
            var c = await _context.Comment.Where(d => d.NewsOneId == id).ToListAsync();
            //var newsToUpdate = await _context.NewsOne.Include(f => f.Comments).ToListAsync();
            //newsToUpdate.Remove()
            //foreach (var item in newsToUpdate)
            //{
            //    item.
                
            //}
            
            if(id == null)
            {
                return NotFound();
            }

            Comment commentToDelete = await _context.Comment.FindAsync(id);
            if(commentToDelete == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(commentToDelete);

            await _context.SaveChangesAsync();

            return RedirectToPage("Edit", new { id = commentToDelete.NewsOneId });
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
           
            var newsToUpdate = await _context.NewsOne.Include(f => f.Comments).FirstOrDefaultAsync(m => m.ID == id);

            if (newsToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<NewsOne>(
                newsToUpdate,
                "newsOne",
                s => s.Title, s => s.Text, s => s.Date, s =>s.Comments))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }

        private bool NewsOneExists(int id)
        {
            return _context.NewsOne.Any(e => e.ID == id);
        }
    }
}
