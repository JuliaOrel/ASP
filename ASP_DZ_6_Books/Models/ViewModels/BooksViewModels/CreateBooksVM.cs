using ASP_DZ_6_Books.Models.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models.ViewModels.BooksViewModels
{
    public class CreateBooksVM
    {
        public BookDTO Book { get; set; }
        public string[] Tags { get; set; }
        [Required]
        public IFormFile Image { get; set; }

    }
}
