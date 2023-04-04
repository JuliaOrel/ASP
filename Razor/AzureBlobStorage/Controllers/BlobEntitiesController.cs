using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureBlobStorage.Data;
using AzureBlobStorage.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AzureBlobStorage.Controllers
{
    public class BlobEntitiesController : Controller
    {
        private readonly AzureBlobStorageContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly string _containerName;

     public BlobEntitiesController(AzureBlobStorageContext context, IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
            _containerName = _configuration.GetSection("Azure:BlobContainerName").Value;
            
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
        public async Task<IActionResult> Create(CreateBlobVM model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString() + model.Image.FileName;
                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
                await blobContainerClient.CreateIfNotExistsAsync();
                await blobContainerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
                BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
                Stream imageStream = model.Image.OpenReadStream();
                await blobClient.UploadAsync(imageStream);
                BlobEntity blobEntityToAdd = new BlobEntity
                {
                    FileName = fileName,
                    Uri = blobClient.Uri.AbsoluteUri,
                };
                await _context.BlobEntities.AddAsync(blobEntityToAdd);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
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
