using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesCities.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string AirPollutionDegree { get; set; }
        public bool IsDeleted { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }

    }
}
