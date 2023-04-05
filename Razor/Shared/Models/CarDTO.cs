using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class CarDTO
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int YearIssue { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Color { get; set; }
        public int? CompanyId { get; set; }
    }
}
