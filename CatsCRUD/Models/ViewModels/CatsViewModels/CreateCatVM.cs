using CatsCRUD.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Models.ViewModels.CatsViewModels
{
    public class CreateCatVM
    {
        public CatDTO Cat { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public SelectList BreedSL { get; set; }
    }
}
