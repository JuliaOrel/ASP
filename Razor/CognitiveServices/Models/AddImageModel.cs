﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Models
{
    public class AddImageModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
