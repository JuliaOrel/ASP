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
        //public async Task<IActionResult> OnPostEditComment(int id)
        //{
        //    var newsToUpdate=await _context.NewsOne.Include(f => f.Comments).FirstOrDefaultAsync(m => m.ID == id);
        //    Comment = await _context.Comment.FirstOrDefaultAsync();
        //    await _context.SaveChangesAsync();
            
        //    return Page();
        //}
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see https://aka.ms/RazorPagesCRUD.
            public async Task<IActionResult> OnPostAsync(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Attach(NewsOne).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!NewsOneExists(NewsOne.ID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");
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
