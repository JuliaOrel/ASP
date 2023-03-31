using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureBlobStorage.Data;
using AzureBlobStorage.Models;

namespace AzureBlobStorage.Controllers
{
    public class BlobEntitiesController : Controller
    {
        private readonly AzureBlobStorageContext _context;

        public BlobEntitiesController(AzureBlobStorageContext context)
        {
            _context = context;
        }

        // GET: BlobEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlobEntities.ToListAsync());
        }

        // GET: BlobEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blobEntity = await _context.BlobEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blobEntity == null)
            {
                return NotFound();
            }

            return View(blobEntity);
        }

        // GET: BlobEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlobEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,Uri")] BlobEntity blobEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blobEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blobEntity);
        }

        // GET: BlobEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blobEntity = await _context.BlobEntities.FindAsync(id);
            if (blobEntity == null)
            {
                return NotFound();
            }
            return View(blobEntity);
        }

        // POST: BlobEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,Uri")] BlobEntity blobEntity)
        {
            if (id != blobEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blobEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlobEntityExists(blobEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blobEntity);
        }

        // GET: BlobEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blobEntity = await _context.BlobEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blobEntity == null)
            {
                return NotFound();
            }

            return View(blobEntity);
        }

        // POST: BlobEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blobEntity = await _context.BlobEntities.FindAsync(id);
            _context.BlobEntities.Remove(blobEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlobEntityExists(int id)
        {
            return _context.BlobEntities.Any(e => e.Id == id);
        }
    }
}
