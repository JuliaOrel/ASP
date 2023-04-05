using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class CarDetailsDTO: CarDTO
    {
        [Display(Name = "Company's name")]
        public string CompanyName { get; set; }
    }
}
