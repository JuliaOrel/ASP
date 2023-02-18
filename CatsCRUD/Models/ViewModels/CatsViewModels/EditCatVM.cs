using CatsCRUD.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCRUD.Models.ViewModels.CatsViewModels
{
    public class EditCatVM
    {
        public CatDTO Cat { get; set; }
        public IFormFile Image { get; set; }
        public SelectList BreedSL { get; set; }
    }
}
