using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
