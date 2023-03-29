using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearIssue { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
