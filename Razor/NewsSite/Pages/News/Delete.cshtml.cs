using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsSite.Data;
using NewsSite.Models;

namespace NewsSite.Pages.News
{
    public class DeleteModel : PageModel
    {
        private readonly NewsSite.Data.NewsBlogContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(NewsSite.Data.NewsBlogContext context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        //public DeleteModel(NewsSite.Data.NewsBlogContext context)
        //{
        //    _context = context;
        //}

        [BindProperty]
        public NewsOne NewsOne { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
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
            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //NewsOne = await _context.NewsOne.FindAsync(id);
            var newsOne = await _context.NewsOne.FindAsync(id);
            //if (NewsOne != null)
            //{
            //    _context.NewsOne.Remove(NewsOne);
            //    await _context.SaveChangesAsync();
            //}

            //return RedirectToPage("./Index");
            if (newsOne == null)
            {
                return NotFound();
            }
            try
            {
                _context.NewsOne.Remove(newsOne);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToAction(
                    "./Delete",
                    new { id, saveChangesError = true });
            }
        }


    }
}

