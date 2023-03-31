using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobStorage.Models
{
    public class EditBlobVM
    {
        public IFormFile Image { get; set; }
        public BlobEntity BlobEntity { get; set; }
    }
}
