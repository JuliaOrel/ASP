using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsSite.Data;
using NewsSite.Models;

namespace NewsSite.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;

        public CreateModel(NewsSite.Data.NewsBlogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NewsOne NewsOne { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NewsOne.Add(NewsOne);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
