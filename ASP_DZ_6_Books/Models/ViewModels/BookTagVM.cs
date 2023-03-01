using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_DZ_6_Books.Models.ViewModels
{
    public class BookTagVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string[]Tags { get; set; }
    }
}
