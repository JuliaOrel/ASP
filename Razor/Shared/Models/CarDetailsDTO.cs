using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class CarDetailsDTO: CarDTO
    {
        [Display(Name = "Company's name")]
        public string CompanyName { get; set; }
    }
}
