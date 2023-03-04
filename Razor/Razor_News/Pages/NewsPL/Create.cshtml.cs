using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor_News.Data;
using Razor_News.Models;

namespace Razor_News.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly Razor_News.Data.Razor_NewsContext _context;

        public CreateModel(Razor_News.Data.Razor_NewsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public News News { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.News.Add(News);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
