using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CognitiveServices.Models;
using CognitiveServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CognitiveServices.Controllers
{
    public class ComputerVisionController : Controller
    {
        private readonly AzureBlobService _azureBlobService;
        private readonly AzureComputerVisionService _azureComputerVisionService;
        public ComputerVisionController(AzureBlobService azureBlobService, AzureComputerVisionService azureComputerVisionService)
        {
            _azureBlobService = azureBlobService;
            _azureComputerVisionService = azureComputerVisionService;
        }
        public async Task<IActionResult> Index()
        {
            IList<GetImageModel> models = new List<GetImageModel>();
            await foreach (BlobItem item in _azureBlobService.BlobContainerClient.GetBlobsAsync())
            {
                using(MemoryStream ms=new MemoryStream())
                {
                    BlobClient blobClient = _azureBlobService.GetBlobClient(item.Name);
                    await blobClient.DownloadToAsync(ms);
                    ms.Position = 0;

                    GetImageModel model = await JsonSerializer.DeserializeAsync<GetImageModel>(ms);
                    if(model is not null)
                    {
                        models.Add(model);
                    }
                }
            }
            return View(models);
        }

        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> AddImage(AddImageModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            byte[] imageData = null;
            using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
            {
                imageData = br.ReadBytes((int)model.Image.Length);
            }

            (string, bool) statusAndIsAdult = await _azureComputerVisionService
                .GetComputerVisionStatus(model.Image.OpenReadStream());
            if(statusAndIsAdult.Item2==true)
            {
                return Content("Your image contains the Adult Cotect. " +
                    "It will not be save to azure blob storage");
            }
            GetImageModel getImageModel = new GetImageModel
            {
                ImageData = imageData,
                ComputerVisionStatus = statusAndIsAdult.Item1
            };
            using(MemoryStream ms=new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(ms, getImageModel);
                ms.Position = 0;
                BlobClient blobClient = _azureBlobService
                    .GetBlobClient(model.Image.FileName);
                await blobClient.UploadAsync(ms);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
