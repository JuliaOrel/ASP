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

        //// GET: BlobEntities/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var blobEntity = await _context.BlobEntities
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (blobEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(blobEntity);
        //}

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
            EditBlobVM editBlobVM = new EditBlobVM
            {
                BlobEntity = blobEntity,
            };

            return View(editBlobVM);
        }

        // POST: BlobEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBlobVM model)
        {
            if (id != model.BlobEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            if(model.Image is null)
            {
                return RedirectToAction(nameof(Index));
            }
            string fileName = Guid.NewGuid().ToString() + model.Image.FileName;
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            BlobClient blobClientToDelete =
                blobContainerClient.GetBlobClient(model.BlobEntity.FileName);
            await blobClientToDelete.DeleteIfExistsAsync();
            
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName); blobClient = blobContainerClient.GetBlobClient(fileName);
            Stream imageStream = model.Image.OpenReadStream();
            await blobClient.UploadAsync(imageStream);
            model.BlobEntity.FileName = fileName;
            model.BlobEntity.Uri = blobClient.Uri.AbsoluteUri;
            _context.BlobEntities.Update(model.BlobEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: BlobEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blobEntity = await _context.BlobEntities
                .FindAsync(id);
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
            if(blobEntity == null)
            {
                return NotFound();
            }
            BlobContainerClient blobContainerClient =
                _blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClientToDelete =
                blobContainerClient.GetBlobClient(blobEntity.FileName);
            await blobClientToDelete.DeleteIfExistsAsync();
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
