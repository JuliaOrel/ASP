using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobStorage.Models
{
    public class CreateBlobVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
