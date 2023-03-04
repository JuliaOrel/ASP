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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NewsOne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsOneExists(NewsOne.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewsOneExists(int id)
        {
            return _context.NewsOne.Any(e => e.ID == id);
        }
    }
}
