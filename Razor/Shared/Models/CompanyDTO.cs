using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
   public class CompanyDTO
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company's name")]
        public string Name { get; set; }
    }
}
